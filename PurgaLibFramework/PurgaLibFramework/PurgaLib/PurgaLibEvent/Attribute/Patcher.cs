using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute
{
    public class Patcher
    {
        private static int patchesCounter;
        public Harmony Harmony { get; }

        public static HashSet<Type> UnpatchedTypes { get; private set; } = GetAllPatchTypes();
        public static HashSet<MethodBase> DisabledPatches { get; } = new();

        internal Patcher()
        {
            Harmony = new Harmony($"purgalib.events.{++patchesCounter}");
        }
        
        public void PatchAll(bool includeEvents, out int failedPatches)
        {
            failedPatches = 0;

            Log.Info("[Patcher] Types to patch:");
            foreach (var t in UnpatchedTypes)
                Log.Info($"  - {t.FullName}");

            IEnumerable<Type> toPatch = includeEvents
                ? UnpatchedTypes
                : UnpatchedTypes.Where(t => !t.GetCustomAttributes<EventPatchAttribute>().Any());

            foreach (Type type in toPatch.ToList())
            {
                try
                {
                    Log.Info($"[Patcher] Patching class: {type.FullName}");
                    
                    var methodsBefore = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    foreach (var m in methodsBefore)
                        Log.Info($"[Before Patch] {type.FullName}.{m.Name}");

                    var patchedMethods = Harmony.CreateClassProcessor(type).Patch();
                    
                    if (patchedMethods != null)
                    {
                        foreach (var m in patchedMethods)
                            Log.Info($"[Patched] {m.DeclaringType?.FullName}.{m.Name}");
                    }
                    else
                    {
                        Log.Warn($"[Patcher] No methods patched in {type.FullName}");
                    }

                    // Controlla disabled patches
                    if (patchedMethods != null && DisabledPatches.Any(x => patchedMethods.Contains(x)))
                        ReloadDisabledPatches();

                    UnpatchedTypes.Remove(type);
                }
                catch (HarmonyException ex)
                {
                    Log.Error($"[Patcher] Failed to patch {type.FullName}\n{ex}");
                    failedPatches++;
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Unexpected error patching {type.FullName}\n{ex}");
                    failedPatches++;
                }
            }

            Log.Info("[Patcher] PatchAll complete.");
        }
        
        public void PatchByEvent(IEvent @event)
        {
            var types = UnpatchedTypes
                .Where(t => t.GetCustomAttributes<EventPatchAttribute>()
                    .Any(a => a.Event == @event))
                .ToList();

            foreach (Type type in types)
            {
                try
                {
                    Log.Info($"[Patcher] Patching {type.FullName} for event {@event.GetType().Name}");

                    var patchedMethods = Harmony.CreateClassProcessor(type).Patch();
                    UnpatchedTypes.Remove(type);

                    if (patchedMethods != null)
                    {
                        foreach (var m in patchedMethods)
                            Log.Info($"[Patched] {m.DeclaringType?.FullName}.{m.Name}");
                    }
                    else
                    {
                        Log.Warn($"[Patcher] No methods patched in {type.FullName}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Failed to patch {type.FullName}\n{ex}");
                }
            }
        }
        
        public void ReloadDisabledPatches()
        {
            foreach (MethodBase method in DisabledPatches)
            {
                try
                {
                    Harmony.Unpatch(method, HarmonyPatchType.All, Harmony.Id);
                    Log.Info($"[Patcher] Unpatched {method.DeclaringType?.Name}.{method.Name}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Failed to unpatch {method.DeclaringType?.Name}.{method.Name}\n{ex}");
                }
            }
        }
        
        public void UnpatchAll()
        {
            Log.Info("[Patcher] Unpatching all patches...");
            Harmony.UnpatchAll(Harmony.Id);
            UnpatchedTypes = GetAllPatchTypes();
            Log.Info("[Patcher] Done.");
        }
        
        private static HashSet<Type> GetAllPatchTypes()
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                    t.GetCustomAttributes<HarmonyPatch>().Any() ||
                    t.GetCustomAttributes<EventPatchAttribute>().Any())
                .ToHashSet();

            Log.Info($"[Patcher] Found {types.Count} patchable types in assembly.");

            return types;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class EventPatchAttribute : System.Attribute
    {
        internal Type HandlerType { get; }
        private readonly string eventName;

        internal EventPatchAttribute(Type handlerType, string eventName)
        {
            HandlerType = handlerType;
            this.eventName = eventName;
        }

        internal IEvent Event
        {
            get
            {
                var prop = HandlerType.GetProperty(eventName, BindingFlags.Public | BindingFlags.Static);
                if (prop == null)
                {
                    Log.Warn($"EventPatchAttribute: Property '{eventName}' not found in {HandlerType.FullName}");
                    return null;
                }

                var value = prop.GetValue(null) as IEvent;
                if (value == null)
                {
                    Log.Warn($"EventPatchAttribute: Property '{eventName}' in {HandlerType.FullName} is not an IEvent");
                }

                return value;
            }
        }
    }
}
