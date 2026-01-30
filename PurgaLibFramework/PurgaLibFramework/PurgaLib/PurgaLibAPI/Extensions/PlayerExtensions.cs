using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class PlayerExtensions
    {
        [Obsolete("Use RoleTypeExtensions instead", false)]
        public static bool IsSCP(this Player player)
        {
            return player.Role == PlayerRoles.RoleTypeId.Scp049 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp096 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp106 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp173 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp079 ||
                   player.Role == PlayerRoles.RoleTypeId.Scp939;
        }
        
        [Obsolete("Use RoleTypeExtensions instead", false)]
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

        public static void BadgeText(this Player player, string text)
        {
            if (player == null)
                return;

            player.ReferenceHub.serverRoles.Network_myText = text;
        }

        public static void BadgeColor(this Player player, string color)
        {
            if (player == null)
                return;

            player.ReferenceHub.serverRoles.Network_myColor = color;
        }

        public static void HealFull(this Player player)
        {
            if (player == null) return;
            player.Heal(player.MaxHealth);
        }

        public static void KillSilent(this Player player)
        {
            if (player == null || !player.IsAlive) return;
            player.Kill("Silent");
        }
    }
}
