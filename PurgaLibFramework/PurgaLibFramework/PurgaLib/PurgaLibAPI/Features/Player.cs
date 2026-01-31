using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using CustomPlayerEffects;
using Hints;
using InventorySystem;
using InventorySystem.Items;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core.Interfaces;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Effects;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Hints;
using RemoteAdmin;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class Player : PActor, IEntity, IWorldObject
    {
        private static readonly Dictionary<ReferenceHub, Player> Cache = new();

        public ReferenceHub ReferenceHub { get; }
        public PlayerEffectHandler EffectHandler { get; }
        public PlayerHint CurrentHint { get; internal set; }
        public bool HasHint => CurrentHint != null;
        public HintDisplay HintDisplay { get; private set; }
        public NetworkConnection Connection { get; set; }
        public Player(ReferenceHub gameObject)
        {
            ReferenceHub = ReferenceHub.GetHub(gameObject);
            EffectHandler = new PlayerEffectHandler(gameObject);
        }

        public override Transform Transform => ReferenceHub.transform;

        public override bool IsAlive =>
            ReferenceHub.IsAlive();
        public bool IsHost => ReferenceHub.isLocalPlayer;
        public bool IsNpc => !IsHost && ReferenceHub.connectionToClient.GetType() != typeof(NetworkConnectionToClient);
        public bool IsConnected => ReferenceHub.connectionToClient != null;
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
        public Team Team => 
            ReferenceHub.roleManager.CurrentRole.Team;
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

        public void Broadcast(PlayerBroadcast broadcast, bool clearPrevious = false)
        {
            if (broadcast == null || !broadcast.Show)
                return;

            if (clearPrevious)
                global::Broadcast.Singleton.TargetClearElements(Connection);

            global::Broadcast.Singleton.TargetAddElement(Connection, broadcast.Content, broadcast.Duration, broadcast.Type);
        }
        
        public void Broadcast(string message, ushort duration = 10, Broadcast.BroadcastFlags type = global::Broadcast.BroadcastFlags.Normal, bool clearPrevious = false)
        {
            Broadcast(new PlayerBroadcast(message, duration, true, type), clearPrevious);
        }
        
        public void ShowHint(string message, float duration = 3f)
        {
            ShowHint(message,
                new HintParameter[] { new StringHintParameter(message) },
                null,
                duration);
        }
        
        public void ShowHint(string message, HintEffect[] hintEffects, float duration = 3f)
        {
            ShowHint(message,
                new HintParameter[] { new StringHintParameter(message) },
                hintEffects,
                duration);
        }
        
        public void ShowHint(string message, HintParameter[] hintParameters, HintEffect[] hintEffects, float duration = 3f)
        {
            if (ReferenceHub == null || ReferenceHub.hints == null)
                return;

            message ??= string.Empty;

            ReferenceHub.hints.Show(new TextHint(
                message,
                (hintParameters != null && hintParameters.Length > 0)
                    ? hintParameters
                    : new HintParameter[] { new StringHintParameter(message) },
                hintEffects,
                duration));
        }
        
        public void ShowHint(PlayerHint hint)
        {
            if (hint == null)
                return;

            ShowHint(hint.Message, hint.Duration);
        }

        public void SendMessage(string message)
            => ReferenceHub.gameConsoleTransmission.SendMessage(message);

        public void Kick(string reason)
            => ReferenceHub.connectionToClient?.Disconnect();

        public void Ban(BanDetails reason, BanHandler.BanType duration)
            => BanHandler.IssueBan(
                reason,
                duration
            );
        
        public bool EnableEffect(Effect effect) 
        {
            if (effect == null)
                return false;

            if (!effect.IsEnabled)
                return false;

            if (!EffectHandler.TryGetEffect(effect.Type, out StatusEffectBase status))
                return false;

            if (effect.AddDurationIfActive && status.IsEnabled)
            {
                status.ServerChangeDuration(effect.Duration, addDuration: true);
            }
            else
            {
                status.Intensity = effect.Intensity;
                status.ServerSetState(effect.Intensity, effect.Duration);
            }

            return true;
        }
        
        public bool DisableEffect(EffectType type)
        {
            if (!EffectHandler.TryGetEffect(type, out StatusEffectBase effect))
                return false;

            effect.ServerDisable();
            return true;
        }
        
        public bool HasEffect(EffectType type)
        {
            return EffectHandler.TryGetEffect(type, out StatusEffectBase effect)
                   && effect.IsEnabled;
        }

        public StatusEffectBase GetEffect(EffectType type)
        {
            EffectHandler.TryGetEffect(type, out StatusEffectBase effect);
            return effect;
        }

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
        public static Player Get(GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            ReferenceHub hub = gameObject.GetComponent<ReferenceHub>();

            if (hub == null)
                return null;

            return Get(hub);
        }
        
        public static bool TryGet(string userId, out Player player)
        {
            player = Get(userId); 
            return player != null;
        }

        internal static void Remove(ReferenceHub hub)
            => Cache.Remove(hub);
        
    }
}
