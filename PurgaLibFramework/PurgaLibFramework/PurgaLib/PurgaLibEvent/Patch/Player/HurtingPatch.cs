using System;
using HarmonyLib;
using PlayerStatsSystem;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player;

[EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnHurting))]
[HarmonyPatch(typeof(PlayerStats), nameof(PlayerStats.DealDamage))]
public static class HurtingPatch
{
    [HarmonyPostfix]
    private static void Postfix(PlayerStats __instance, ref float damage, PlayerStats target)
    {
        try
        {
            var attackerHub = __instance.gameObject.GetComponent<ReferenceHub>();
            var victimHub = target?.gameObject.GetComponent<ReferenceHub>();

            if (attackerHub == null || victimHub == null)
                return;

            var attacker = new global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player(attackerHub);
            var victim = new global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player(victimHub);
            
            PlayerHandler.OnHurting(new PlayerHurtingEventArgs(attacker, victim, damage));
        }
        catch (Exception e)
        {
            Log.Error($"Error in HurtingPatch Postfix: {e}");
        }
    }
    
}
