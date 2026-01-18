using System;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using PurgaLibEvents.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Door;

[EventPatch(typeof(DoorHandler), nameof(DoorHandler.OnInteracting))]
[HarmonyPatch(typeof(DoorVariant), nameof(DoorVariant.ServerInteract), typeof(ReferenceHub), typeof(byte))]
public static class InteractingDoorPatch
{
    [HarmonyPrefix]
    private static void Prefix(DoorVariant __instance, ReferenceHub hub, byte colliderId)
    {
        try
        {
            if (__instance == null || hub == null)
                return;
            
            var player = new PurgaLibAPI.Features.Player(hub);
            var door = new global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Door(__instance);
            
            DoorHandler.OnInteracting(new DoorInteractingEventArgs(player, door));
        }
        catch (Exception e)
        {
            Log.Error($"Error in InteractingPatch Prefix: {e}");
        }
    }
}
