using System;
using HarmonyLib;
using PlayerStatsSystem;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnDied))]
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnDying))]
    [HarmonyPatch(typeof(PlayerStats), nameof(PlayerStats.KillPlayer))]
    public static class KillPlayerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(PlayerStats __instance, DamageHandlerBase handler)
        {
            try
            {
                var hub = __instance._hub;
                if (hub == null) return;

                var victim = new PurgaLibAPI.Features.Player(hub);
                var attacker = GetAttacker(handler);
                var reason = handler?.ServerLogsText ?? "Unknown";

                PlayerHandler.OnDying(new PlayerDyingEventArgs(victim, attacker, reason));

                Log.Success($"[Prefix:{nameof(PlayerStats.KillPlayer)}] {victim.Nickname} is about to die, attacker: {attacker?.Nickname ?? "None"}");
            }
            catch (Exception e)
            {
                Log.Error($"[KillPlayerPatch:Prefix] {e}");
            }
        }

        [HarmonyPostfix]
        public static void Postfix(PlayerStats __instance, DamageHandlerBase handler)
        {
            try
            {
                var hub = __instance._hub; 
                if (hub == null) return;

                var victim = new PurgaLibAPI.Features.Player(hub);
                var attacker = GetAttacker(handler);

                PlayerHandler.OnDied(new PlayerDiedEventArgs(victim, attacker, handler?.GetHashCode() ?? 0));

                Log.Success($"[Postfix:{nameof(PlayerStats.KillPlayer)}] {victim.Nickname} has died, attacker: {attacker?.Nickname ?? "None"}");
            }
            catch (Exception e)
            {
                Log.Error($"[KillPlayerPatch:Postfix] {e}");
            }
        }

        private static PurgaLibAPI.Features.Player GetAttacker(DamageHandlerBase handler)
        {
            if (handler is AttackerDamageHandler attackerHandler && attackerHandler.Attacker.Hub != null)
                return new PurgaLibAPI.Features.Player(attackerHandler.Attacker.Hub);

            return null;
        }
    }
}
