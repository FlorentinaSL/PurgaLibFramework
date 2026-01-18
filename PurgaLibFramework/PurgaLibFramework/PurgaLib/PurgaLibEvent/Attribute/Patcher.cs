using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute
{
    /// <summary>
    /// Harmony patcher per PurgaLib (stile Exiled).
    /// Patcha tutte le classi con HarmonyPatch o EventPatchAttribute automaticamente.
    /// </summary>
    public class Patcher
    {
        private static int patchesCounter;

        public Harmony Harmony { get; }

        /// <summary>
        /// Tutti i tipi ancora non patchati di PurgaLib
        /// </summary>
        public static HashSet<Type> UnpatchedTypes { get; private set; } = GetAllPatchTypes();

        /// <summary>
        /// Metodi per cui il patch automatico è disabilitato
        /// </summary>
        public static HashSet<MethodBase> DisabledPatches { get; } = new();

        public Patcher()
        {
            Harmony = new Harmony($"purgalib.events.{++patchesCounter}");
        }

        // ------------------- PATCH ALL (stile Exiled) -------------------
        public void PatchAll(bool includeEvents, out int failedPatches)
        {
            failedPatches = 0;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    // Patcha tutte le classi Harmony nell'assembly
                    Harmony.PatchAll(assembly);

                    // Aggiorna UnpatchedTypes
                    UnpatchedTypes.RemoveWhere(t => t.Assembly == assembly);

                    Log.Info($"[Patcher] Patched assembly: {assembly.FullName}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Failed to patch assembly {assembly.FullName}: {ex}");
                    failedPatches++;
                }
            }

            if (!includeEvents)
            {
                // Rimuovi tipi con EventPatchAttribute se non vogliamo includere eventi
                UnpatchedTypes.RemoveWhere(t => t.GetCustomAttributes<EventPatchAttribute>().Any());
            }
        }

        // ------------------- PATCH BY EVENT -------------------
        public void PatchByEvent<TEvent>()
        {
            var types = UnpatchedTypes
                .Where(t => t.GetCustomAttributes<EventPatchAttribute>()
                             .Any(a => (a as EventPatchAttribute)?.TypeId == typeof(TEvent)))
                .ToList();

            foreach (var type in types)
            {
                try
                {
                    Harmony.CreateClassProcessor(type).Patch();
                    UnpatchedTypes.Remove(type);
                    Log.Info($"[Patcher] Patched {type.FullName} for event {typeof(TEvent).Name}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Failed to patch {type.FullName} for event {typeof(TEvent).Name}: {ex}");
                }
            }
        }

        // ------------------- RELOAD DISABLED -------------------
        public void ReloadDisabledPatches()
        {
            foreach (var method in DisabledPatches)
            {
                try
                {
                    Harmony.Unpatch(method, HarmonyPatchType.All, Harmony.Id);
                    Log.Info($"[Patcher] Unpatched {method.DeclaringType?.Name}.{method.Name}");
                }
                catch (Exception ex)
                {
                    Log.Error($"[Patcher] Failed to unpatch {method.Name}: {ex}");
                }
            }
        }

        // ------------------- UNPATCH ALL -------------------
        public void UnpatchAll()
        {
            Log.Info("[Patcher] Unpatching all...");
            Harmony.UnpatchAll(Harmony.Id);
            UnpatchedTypes = GetAllPatchTypes();
            Log.Info("[Patcher] All patches removed!");
        }

        // ------------------- HELPERS -------------------
        private static HashSet<Type> GetAllPatchTypes()
        {
            var types = new HashSet<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] assemblyTypes;
                try { assemblyTypes = assembly.GetTypes(); }
                catch (ReflectionTypeLoadException ex) { assemblyTypes = ex.Types.Where(t => t != null).ToArray(); }

                foreach (var type in assemblyTypes)
                {
                    if (type == null || string.IsNullOrEmpty(type.Namespace))
                        continue;

                    if (!type.Namespace.StartsWith("PurgaLibFramework"))
                        continue;
                    
                    foreach (var method in type.GetMethods())
                    {
                        Console.WriteLine($"Found method: {type.FullName}.{method.Name}");
                    }

                    if (type.GetCustomAttributes(typeof(HarmonyPatch), true).Any() ||
                        type.GetCustomAttributes(typeof(EventPatchAttribute), true).Any())
                    {
                        types.Add(type);
                    }
                }

            }

            return types;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class EventPatchAttribute : System.Attribute
    {
        internal Type TypeId { get; }
        private readonly string eventName;

        internal EventPatchAttribute(Type handlerType, string eventName)
        {
            TypeId = handlerType;
            this.eventName = eventName;
        }

        internal IEvent Event => (IEvent)TypeId.GetProperty(eventName)?.GetValue(null);
    }
}
