using System;
using HarmonyLib;
using InventorySystem.Searching;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.EventsArgs;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player;

[EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnItemPickedUp))]
[HarmonyPatch(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.Complete))]
public static class PickUpPatch
{
    [HarmonyPostfix]
    private static void Postfix(ItemSearchCompletor __instance)
    {
        try
        {
            var hub = __instance.Hub;
            if (hub == null) return;

            var player = new PurgaLibAPI.Features.Player(hub);
            
            var item = new PurgaLibAPI.Features.Item(__instance.TargetPickup);
            if (item == null) return;
            
            PlayerHandler.OnItemPickedUp(
                new CustomItemPickedUpEventArgs(player, item)
            );
        }
        catch (Exception e)
        {
            Log.Error($"Error in PickUpPatch Postfix: {e}");
        }
    }
}
