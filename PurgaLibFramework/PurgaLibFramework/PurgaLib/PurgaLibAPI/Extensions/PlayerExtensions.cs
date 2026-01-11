using UnityEngine;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class PlayerExtensions
    {
        public static bool IsSCP(this Player player)
        {
            return player.Role == PlayerRoles.RoleTypeId.Scp049 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp096 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp106 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp173 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp079 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp939;
        }

        public static bool IsHuman(this Player player)
        {
            return !player.IsSCP() && player.Role != PlayerRoles.RoleTypeId.Spectator;
        }

        public static void TeleportTo(this Player player, Player target, Vector3 offset)
        {
            if (player == null || target == null) return;
            player.Position = target.Position + offset;
        }

        public static void TeleportTo(this Player player, Player target)
        {
            TeleportTo(player, target, Vector3.zero);
        }

        public static void HealFull(this Player player)
        {
            if (player == null) return;
            player.Heal(100f - player.Health);
        }

        public static void KillSilent(this Player player)
        {
            if (player == null || !player.IsAlive) return;
            player.Kill("Silent");
        }
    }
}
