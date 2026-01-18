using System;
using HarmonyLib;
using Interactables.Interobjects;
using PurgaLibEvents.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Elevator;

[EventPatch(typeof(ElevatorHandler), nameof(ElevatorHandler.OnInteracting))]
[HarmonyPatch(typeof(ElevatorDoor), nameof(ElevatorDoor.ServerInteract), typeof(ReferenceHub), typeof(byte))]
public static class InteractingElevatorPatch
{
    [HarmonyPrefix]
     private static void Prefix(ElevatorDoor __instance, ReferenceHub hub, byte colliderId)
    {
        try
        {
            if (__instance == null || hub == null)
                return;
            
            var player = new PurgaLibAPI.Features.Player(hub);
            var elevator = new global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Elevator(__instance);
            
            ElevatorHandler.OnInteracting(new ElevatorUsingEventArgs(player, elevator));
        }
        catch (Exception e)
        {
            Log.Error($"Error in InteractingPatch Prefix: {e}");
        }
    }
}