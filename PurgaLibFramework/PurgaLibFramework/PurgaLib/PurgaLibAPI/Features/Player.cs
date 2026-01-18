using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Hints;
using InventorySystem;
using InventorySystem.Items;
using PlayerRoles;
using PlayerStatsSystem;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core.Interfaces;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using RemoteAdmin;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class Player : PActor, IEntity, IWorldObject
    {
        private static readonly Dictionary<ReferenceHub, Player> Cache = new();

        public ReferenceHub ReferenceHub { get; }

        public Player(ReferenceHub gameObject)
        {
            ReferenceHub = ReferenceHub.GetHub(gameObject);
        }

        public override Transform Transform => ReferenceHub.transform;

        public override bool IsAlive =>
            ReferenceHub.IsAlive();
        
        #pragma warning disable CS0108
        public Vector3 Position
        {
            get => ReferenceHub.transform.position;
            set => ReferenceHub.transform.position = value;
        }

        public string UserId => ReferenceHub.authManager.UserId;
        public string Nickname => ReferenceHub.nicknameSync.DisplayName;
        public int PlayerId => ReferenceHub.PlayerId;

        public float Health =>
            ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
        public float MaxHealth =>
            ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;
        public float MinHealth =>
            ReferenceHub.playerStats.GetModule<HealthStat>().MinValue;
        public RoleTypeId Role =>
            ReferenceHub.roleManager.CurrentRole.RoleTypeId;

        public Inventory Inventory =>
            ReferenceHub.inventory;
        
        public void Kill(string reason = null)
        {
            Hurt(null, 100000, DamageType.None, reason);
        }
        public void Hurt(Player attacker, float amount, DamageType damageType = DamageType.None, string cassieAnnouncement = null) =>
            Hurt(new DamageHandler.DamageHandler(this, attacker, amount, damageType, cassieAnnouncement));
         
        public void Hurt(DamageHandlerBase damageHandlerBase) => ReferenceHub.playerStats.DealDamage(damageHandlerBase);
        public void Heal(float amount, bool overrideMaxHealth = false)
        {
            var healthStat = ReferenceHub.playerStats.GetModule<HealthStat>();

            if (!overrideMaxHealth)
                healthStat.ServerHeal(amount);
            else
                healthStat.CurValue += amount;
        }


        public void GiveItem(ItemType item)
            => ReferenceHub.inventory.ServerAddItem(item, ItemAddReason.Undefined);

        public void SetRole(RoleTypeId role)
            => ReferenceHub.roleManager.ServerSetRole(role, RoleChangeReason.RemoteAdmin);

        public void Teleport(Vector3 position)
            => ReferenceHub.transform.position = position;

        public void SendBroadcast(string message, ushort duration)
            => ReferenceHub.BroadcastMessage(message, duration);

        public void SendHint(string message)
            => ReferenceHub.hints.Show(new TextHint(message));

        public void SendMessage(string message)
            => ReferenceHub.gameConsoleTransmission.SendMessage(message);

        public void Kick(string reason)
            => ReferenceHub.connectionToClient?.Disconnect();

        public void Ban(BanDetails reason, BanHandler.BanType duration)
            => BanHandler.IssueBan(
                reason,
                duration
            );

        public static IReadOnlyCollection<Player> List =>
            ReferenceHub.AllHubs
                .Select(Get)
                .Where(p => p != null)
                .ToList();

        public static int Count =>
            ReferenceHub.AllHubs.Count;

        public static Player Get(ReferenceHub hub)
        {
            if (hub == null)
                return null;

            if (!Cache.TryGetValue(hub, out var player))
            {
                player = new Player(hub);
                Cache.Add(hub, player);
            }

            return player;
        }

        public static Player Get(int playerId) =>
            Get(ReferenceHub.AllHubs.FirstOrDefault(h => h.PlayerId == playerId));

        public static Player Get(string userId) =>
            Get(ReferenceHub.AllHubs.FirstOrDefault(h => h.authManager.UserId == userId));

        public static Player Get(ICommandSender sender)
        {
            if (sender is PlayerCommandSender pcs)
                return Get(pcs.ReferenceHub);

            return null;
        }

        internal static void Remove(ReferenceHub hub)
            => Cache.Remove(hub);
    }
}
