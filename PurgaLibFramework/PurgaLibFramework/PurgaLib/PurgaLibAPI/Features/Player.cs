using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core.Interfaces;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class Player : PActor, IEntity, IWorldObject
    {
        internal LabApi.Features.Wrappers.Player Base { get; }

        private static readonly Dictionary<LabApi.Features.Wrappers.Player, Player> Cache = new();

        public Player(LabApi.Features.Wrappers.Player player)
        {
            Base = player;
        }

        public override Transform Transform => Base.GameObject?.transform;
        public override bool IsAlive => Base.IsAlive;

        public new Vector3 Position
        {
            get => Base.Position;
            set => Base.Position = value;
        }

        public string UserId => Base.UserId;
        public string Nickname => Base.Nickname;
        public int PlayerId => Base.PlayerId;
        public float Health => Base.Health;
        public RoleTypeId Role => Base.Role;
        public Inventory Inventory => Base.Inventory;
        public ReferenceHub ReferenceHub => Base.ReferenceHub;

        public void Kill(string reason = null)
            => Base.Kill(reason ?? throw new ArgumentNullException(nameof(reason)));

        public void Heal(float amount)
            => Base.Heal(amount);

        public void GiveItem(ItemType item)
            => Base.AddItem(item);

        public void SetRole(RoleTypeId role)
            => Base.SetRole(role);

        public void Teleport(Vector3 position)
            => Base.Position = position;

        public void Teleport(float x, float y, float z)
            => Base.Position = new Vector3(x, y, z);

        public void TeleportRelative(Vector3 offset)
            => Base.Position += offset;

        public void SendBroadcast(string message, ushort duration)
            => Base.SendBroadcast(message, duration);

        public void SendHint(string message, float duration = 3f)
            => Base.SendHint(message, duration);

        public void SendMessage(string message)
            => Base.SendConsoleMessage(message);
        public void Kick(string reason)
            => Base.Kick(reason ?? throw new ArgumentNullException(nameof(reason)));
        public void Ban(string reason, long duration)
            => Base.Ban(reason, duration);
        public static IReadOnlyCollection<Player> List =>
            LabApi.Features.Wrappers.Player.List
                .Select(Get)
                .Where(p => p != null)
                .ToList();

        public static int Count =>
            LabApi.Features.Wrappers.Player.List.Count;

        public static Player Get(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var lab = LabApi.Features.Wrappers.Player.List
                .FirstOrDefault(p => p.UserId == userId);

            return Get(lab);
        }

        public static Player Get(int playerId)
        {
            var lab = LabApi.Features.Wrappers.Player.List
                .FirstOrDefault(p => p.PlayerId == playerId);

            return Get(lab);
        }

        public static Player Get(LabApi.Features.Wrappers.Player player)
        {
            if (player == null)
                return null;

            if (!Cache.TryGetValue(player, out var wrapped))
            {
                wrapped = new Player(player);
                Cache.Add(player, wrapped);
            }

            return wrapped;
        }

        public static IReadOnlyCollection<Player> GetSpectatorsOf(Player target)
        {
            if (target == null)
                return new List<Player>();

            return LabApi.Features.Wrappers.Player.List
                .Where(p => p.CurrentSpectators.Contains(target.Base))
                .Select(Get)
                .Where(p => p != null)
                .ToList();
        }

        internal static void Remove(LabApi.Features.Wrappers.Player player)
            => Cache.Remove(player);
    }
}
