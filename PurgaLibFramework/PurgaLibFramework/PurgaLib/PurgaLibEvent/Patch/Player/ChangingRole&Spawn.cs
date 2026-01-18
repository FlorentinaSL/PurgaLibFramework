using System;
using HarmonyLib;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Player
{
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnChangedRole))]
    [EventPatch(typeof(PlayerHandler), nameof(PlayerHandler.OnChangingRole))]
    [HarmonyPatch(typeof(PlayerRoleManager), nameof(PlayerRoleManager.InitializeNewRole))]
    public static class ChangingRoleSpawnPatch
    {
        [HarmonyPrefix]
        private static void Prefix(
            PlayerRoleManager __instance,
            RoleTypeId targetId,
            out RoleTypeId __state)
        {
            __state = __instance.CurrentRole != null ? __instance.CurrentRole.RoleTypeId : RoleTypeId.None;

            try
            {
                var hub = __instance.Hub;
                if (hub == null) return;

                var player = new PurgaLibAPI.Features.Player(hub);

                PlayerHandler.OnSpawning(new PlayerSpawningEventArgs(player));
                PlayerHandler.OnChangingRole(new PlayerChangingRoleEventArgs(player, __state, targetId));
            }
            catch (Exception e)
            {
                Log.Error($"Error in ChangingRoleSpawnPatch Prefix: {e}");
            }
        }
        
        [HarmonyPostfix]
        private static void Postfix(
            PlayerRoleManager __instance,
            RoleTypeId targetId,
            RoleChangeReason reason,
            RoleSpawnFlags spawnFlags,
            Mirror.NetworkReader data,
            RoleTypeId __state)
        {
            try
            {
                var hub = __instance.Hub;
                if (hub == null) return;

                var player = new PurgaLibAPI.Features.Player(hub);

                PlayerHandler.OnSpawned(new PlayerSpawnedEventArgs(player));
                PlayerHandler.OnChangedRole(new PlayerChangedRoleEventArgs(
                    player,
                    __state,
                    targetId,
                    reason,
                    spawnFlags
                ));
            }
            catch (Exception e)
            {
                Log.Error($"Error in ChangingRoleSpawnPatch Postfix: {e}");
            }
        }
    }
}
