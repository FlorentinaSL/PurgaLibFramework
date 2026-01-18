using System;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnBanned))]
    [HarmonyPatch(typeof(BanHandler), nameof(BanHandler.IssueBan))]
    public static class BannedPatch
    {
        [HarmonyPostfix]
        private static void Postfix(bool __result, BanDetails ban, BanHandler.BanType banType, bool forced)
        {
            try
            {
                if (!__result || ban == null || string.IsNullOrEmpty(ban.Id))
                    return;

                var player = PurgaLibAPI.Features.Player.Get(ban.Id);

                long duration = ban.Expires;

                PlayerHandler.OnBanned(
                    new PlayerBannedEventArgs(player, ban.Reason, duration)
                );
            }
            catch (Exception e)
            {
                Log.Error($"Error in BannedPatch Postfix: {e}");
            }
        }
    }
}
