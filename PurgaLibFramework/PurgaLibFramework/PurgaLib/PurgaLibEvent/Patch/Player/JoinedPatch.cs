using System;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player   
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnJoined))]
    [HarmonyPatch(typeof(ReferenceHub), nameof(ReferenceHub.Start))]
    internal static class JoinedPatch
    {
        [HarmonyPostfix]
        private static void Postfix(ReferenceHub __instance)
        {
            try
            {
                if (__instance == null)
                    return;

                var player = new PurgaLibAPI.Features.Player(__instance);

                PlayerHandler.OnJoined(new PlayerJoinedEventArgs(player));

                Log.Success("JOINED PATCH CALLED");
            }
            catch (Exception e)
            {
                Log.Error($"JoinedPatch error:\n{e}");
            }
        }
    }

}
