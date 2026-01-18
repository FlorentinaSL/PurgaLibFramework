using System;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player;

[EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnKicked))]
[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.Disconnect), typeof(GameObject), typeof(string))]
public static class KickedPatch
{
    [HarmonyPostfix]
    private static void Postfix(GameObject playerObject, string reason)
    {
        try
        {
            if (playerObject == null) return;
            
            var hub = playerObject.GetComponent<ReferenceHub>();
            var player = hub != null ? new PurgaLibAPI.Features.Player(hub) : null;

            if (hub != null)
                player = new PurgaLibAPI.Features.Player(hub);
            
            PlayerHandler.OnKicked(new PlayerKickedEventArgs(player, reason));
        }
        catch (Exception e)
        {
            Log.Error($"Error in KickedPatch Postfix: {e}");
        }
    }
}
