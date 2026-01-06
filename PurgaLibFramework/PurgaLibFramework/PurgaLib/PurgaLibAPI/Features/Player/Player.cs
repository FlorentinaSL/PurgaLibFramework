using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using UnityEngine;
using PlayerRoles;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player
{
    public class Player
    {
        internal LabApi.Features.Wrappers.Player Base { get; }
        public ReferenceHub RefHub => Base.ReferenceHub;


        internal Player(LabApi.Features.Wrappers.Player player)
        {
            Base = player;
        }

        public string UserId => Base.UserId;
        public string Nickname => Base.Nickname;
        public bool IsAlive => Base.IsAlive;
        public float Health => Base.Health;
        public RoleTypeId Role => Base.Role;
        public Vector3 Position
        {
            get => Base.Position;
            set => Base.Position = value;
        }
        
        public void Kill(string reason) => Base.Kill(reason);
        public void Heal(float amount) => Base.Heal(amount);
        public void GiveItem(ItemType item) => Base.AddItem(item);
        public void SetRole(RoleTypeId role) => Base.SetRole(role);

        public void Teleport(Vector3 position) => Base.Position = position;
        public void Teleport(float x, float y, float z) => Base.Position.Set(x, y, z);
        public void TeleportRelative(float dx, float dy, float dz)
        {
            var pos = Base.Position;
            Base.Position.Set(pos.x + dx, pos.y + dy, pos.z + dz);
        }

        public static Player Get(LabApi.Features.Wrappers.Player player) => player == null ? null : new Player(player);
        
        public static class Manager
        {
            public static IReadOnlyCollection<Player> List =>
                LabApi.Features.Wrappers.Player.List
                    .Select(Player.Get)
                    .Where(p => p != null)
                    .ToList();

            public static int Count => LabApi.Features.Wrappers.Player.List.Count;

            public static Player Get(string userId)
            {
                if (string.IsNullOrEmpty(userId)) return null;

                var lab = LabApi.Features.Wrappers.Player.List
                    .FirstOrDefault(p => p.UserId == userId);

                return lab == null ? null : Player.Get(lab);
            }
        }
        
        public static class State
        {
            public static string Name(Player player) => player?.Nickname;
            public static int Id(Player player) => player?.Base.PlayerId ?? -1;
            public static RoleTypeId Role(Player player) => player?.Role ?? RoleTypeId.None;
            public static float Health(Player player) => player?.Health ?? 0f;
            public static Vector3 Position(Player player) => player?.Position ?? Vector3.zero;
            public static bool IsAlive(Player player) => player?.IsAlive ?? false;
            public static Inventory Inventory(Player player)
            {
                if (player == null || player.Base == null) return null;
                return player.Base.Inventory;
            }
            
        }
        
        public static class Utility
        {
            public static void SendBroadcast(Player player, string message, ushort duration)
            {
                if (player == null || string.IsNullOrEmpty(message)) return;
                player.Base.SendBroadcast(message, duration);
            }

            public static void SendHint(Player player, string message, float duration)
            {
                if (player == null || string.IsNullOrEmpty(message)) return;
                player.Base.SendHint(message, duration);
            }

            public static void SendMessage(Player player, string message)
            {
                if (player == null || string.IsNullOrEmpty(message)) return;
                player.Base.SendConsoleMessage(message);
            }
        }
    }
}
