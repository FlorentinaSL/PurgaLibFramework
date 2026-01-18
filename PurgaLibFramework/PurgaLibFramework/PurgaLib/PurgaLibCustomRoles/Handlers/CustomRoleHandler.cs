using System.Collections.Generic;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomRoles.Handlers
{
    public static class CustomRoleHandler
    {
        public static readonly List<CustomRole> Registered = new();

        public static Event<Player> Assigned = new();
        public static Event<Player> Removed = new();
        
        public static void Register(CustomRole role)
        {
            if (role == null || Registered.Contains(role)) return;
            Registered.Add(role);
        }
        
        public static void Give(Player player, CustomRole role)
        {
            if (player == null || role == null) return;

            role.OnAssign(player);
            Assigned?.Invoke(player);
        }
        
        public static void Remove(Player player, CustomRole role)
        {
            if (player == null || role == null) return;

            role.OnRemove(player);
            Removed?.Invoke(player);
        }
        
        public static void OnPlayerJoin(Player player)
        {
            var defaultRole = GetDefaultRole();
            Give(player, defaultRole);
        }

        private static CustomRole GetDefaultRole()
        {
            return new CustomRole("default", "Player", "", "White", RoleTypeId.ClassD);
        }
    }
}
