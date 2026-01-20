using System;
using CentralAuth;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnVerified))]
    [HarmonyPatch(typeof(PlayerAuthenticationManager), nameof(PlayerAuthenticationManager.FinalizeAuthentication))]
    public static class VerifiedPatch
    {
        [HarmonyPostfix]
        private static void PlayerVerified(ReferenceHub hub)
        {
            try
            {
                VerifiedPlayersCache.Verified.Add(hub._playerId.ToString());
                
                var player = new PurgaLibAPI.Features.Player(hub);
                PlayerHandler.OnVerified(new PlayerVerifiedEventArgs(player));

                Log.Info($"Player {hub._playerId} has been successfully verified.");
            }
            catch (Exception e)
            {
                Log.Error($"Error in VerifiedPatch PlayerVerified: {e}");
            }
        }
    }
}
