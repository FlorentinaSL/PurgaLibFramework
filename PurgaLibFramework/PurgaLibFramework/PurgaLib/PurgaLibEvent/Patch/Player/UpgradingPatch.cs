using System;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;
using Scp914;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnUpgrading))]
    [HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPlayer))]
    public static class UpgradingPatch
    {
        [HarmonyPrefix]
        public static void Prefix(ReferenceHub playerHub, Scp914KnobSetting knobSetting)
        {
            try
            {
                if (playerHub == null) return;
                
                var player = new PurgaLibAPI.Features.Player(playerHub);
                
                PlayerHandler.OnUpgrading(new UpgradingPlayersEventArgs(player, knobSetting));
                
                Log.Success($"UpgradingPatch called for {nameof(playerHub)}: {playerHub.nicknameSync.Network_myNickSync}, knob: {knobSetting}");
            }
            catch (Exception e)
            {
                Log.Error($"[UpgradingPatch:Prefix] Error in {nameof(Scp914Upgrader.ProcessPlayer)}: {e}");
            }
        }
    }
}
