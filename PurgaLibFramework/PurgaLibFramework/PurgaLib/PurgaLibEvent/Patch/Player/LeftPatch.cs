using System;
using HarmonyLib;
using Mirror;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player;

[EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnLeft))]
[HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.OnServerDisconnect), typeof(NetworkConnectionToClient))]
public static class LeftPatch
{
    [HarmonyPostfix]
    private static void Postfix(CustomNetworkManager __instance, NetworkConnectionToClient conn)
    {
        try
        {
            var hub = conn.identity?.GetComponent<ReferenceHub>();
            if (hub == null) return;
            var player = new PurgaLibAPI.Features.Player(hub);
            PlayerHandler.OnLeft(new PlayerLeftEventArgs(player));
        }
        catch (Exception e)
        {
            Log.Error($"Error in LeftPatch Postfix: {e}");
        }
    }
}
