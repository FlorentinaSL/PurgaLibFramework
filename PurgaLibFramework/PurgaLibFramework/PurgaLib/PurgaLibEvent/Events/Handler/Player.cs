using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PlayerBannedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerBannedEventArgs;
using PlayerChangingRoleEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerChangingRoleEventArgs;
using PlayerDyingEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerDyingEventArgs;
using PlayerHurtingEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerHurtingEventArgs;
using PlayerJoinedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerJoinedEventArgs;
using PlayerKickedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerKickedEventArgs;
using PlayerLeftEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerLeftEventArgs;
using PlayerSpawnedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawnedEventArgs;
using PlayerSpawningEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawningEventArgs;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class PlayerHandler
    {
        private static EventHandler<PlayerJoinedEventArgs> _joined;
        private static EventHandler<PlayerBannedEventArgs> _banned;
        private static EventHandler<PlayerKickedEventArgs> _kicked;
        private static EventHandler<PlayerLeftEventArgs> _left;
        private static EventHandler<PlayerHurtingEventArgs> _hurting;
        private static EventHandler<PlayerDyingEventArgs> _dying;
        private static EventHandler<PlayerChangingRoleEventArgs> _changingRole;
        private static EventHandler<PlayerChangedRoleEventArgs> _changedRole;
        private static EventHandler<PlayerSpawningEventArgs> _spawning;
        private static EventHandler<PlayerSpawnedEventArgs> _spawned;
        private static EventHandler<PlayerPickingUpItemEventArgs> _pickingUpItem;
        private static EventHandler<PlayerDroppingItemEventArgs> _droppingItem;
        private static EventHandler<PlayerUsingItemEventArgs> _usingItem;
        private static EventHandler<PlayerInteractingDoorEventArgs> _interactingDoor;
        private static EventHandler<PlayerInteractingElevatorEventArgs> _interactingElevator;
        private static EventHandler<PlayerVerifiedEventArgs> _verified;
        private static EventHandler<UpgradingPlayersEventArgs> _upgrading;
        
        public static event EventHandler<UpgradingPlayersEventArgs> Upgrading
        {
            add { Add(ref _upgrading, value, RegisterLabApi); }
            remove { _upgrading -= value; }
        }
        public static event EventHandler<PlayerBannedEventArgs> Banned
        {
            add { Add(ref _banned, value, RegisterLabApi);}
            remove { _banned -= value; }
        } 
        public static event EventHandler<PlayerKickedEventArgs> Kicked
        {
            add{ Add(ref _kicked, value, RegisterLabApi); }
            remove { _kicked -= value; }
        } 
        public static event EventHandler<PlayerJoinedEventArgs> Joined
        {
            add { Add(ref _joined, value, RegisterLabApi); }
            remove { _joined -= value; }
        }

        public static event EventHandler<PlayerLeftEventArgs> Left
        {
            add { Add(ref _left, value, RegisterLabApi); }
            remove { _left -= value; }
        }

        public static event EventHandler<PlayerHurtingEventArgs> Hurting
        {
            add { Add(ref _hurting, value, RegisterLabApi); }
            remove { _hurting -= value; }
        }

        public static event EventHandler<PlayerDyingEventArgs> Dying
        {
            add { Add(ref _dying, value, RegisterLabApi); }
            remove { _dying -= value; }
        }

        public static event EventHandler<PlayerChangingRoleEventArgs> ChangingRole
        {
            add { Add(ref _changingRole, value, RegisterLabApi); }
            remove { _changingRole -= value; }
        }

        public static event EventHandler<PlayerChangedRoleEventArgs> ChangedRole
        {
            add { Add(ref _changedRole, value, RegisterLabApi); }
            remove { _changedRole -= value; }
        }

        public static event EventHandler<PlayerSpawningEventArgs> Spawning
        {
            add { Add(ref _spawning, value, RegisterLabApi); }
            remove { _spawning -= value; }
        }

        public static event EventHandler<PlayerSpawnedEventArgs> Spawned
        {
            add { Add(ref _spawned, value, RegisterLabApi); }
            remove { _spawned -= value; }
        }

        public static event EventHandler<PlayerPickingUpItemEventArgs> PickingUpItem
        {
            add { Add(ref _pickingUpItem, value, RegisterLabApi); }
            remove { _pickingUpItem -= value; }
        }

        public static event EventHandler<PlayerDroppingItemEventArgs> DroppingItem
        {
            add { Add(ref _droppingItem, value, RegisterLabApi); }
            remove { _droppingItem -= value; }
        }

        public static event EventHandler<PlayerUsingItemEventArgs> UsingItem
        {
            add { Add(ref _usingItem, value, RegisterLabApi); }
            remove { _usingItem -= value; }
        }

        public static event EventHandler<PlayerInteractingDoorEventArgs> InteractingDoor
        {
            add { Add(ref _interactingDoor, value, RegisterLabApi); }
            remove { _interactingDoor -= value; }
        }

        public static event EventHandler<PlayerInteractingElevatorEventArgs> InteractingElevator
        {
            add { Add(ref _interactingElevator, value, RegisterLabApi); }
            remove { _interactingElevator -= value; }
        }
        
        public static event EventHandler<PlayerVerifiedEventArgs> Verified
        {
            add { Add(ref _verified, value, RegisterLabApi); }
            remove { _verified -= value; }
        }

        private static void Add<T>(ref EventHandler<T> field, EventHandler<T> handler, Action register) where T : System.EventArgs
        {
            bool wasEmpty = field == null;
            field += handler;
            if (wasEmpty)
                register();
        }

        private static void On<T>(EventHandler<T> field, T args) where T : System.EventArgs
        {
            field?.Invoke(null, args);
        }

        public static void RegisterLabApi()
        {
            PlayerEvents.Joined += ev =>
            {
                var player = ev.Player;
                On(_joined, new PlayerJoinedEventArgs(ev.Player));

                string userId = player.UserId;
                if (string.IsNullOrEmpty(userId))
                    return;

                if (VerifiedPlayersCache.Verified.Add(userId))
                    On(_verified, new PlayerVerifiedEventArgs(player));

                if (CreditTagsHandler.IsContributor(player))
                    CreditTagsHandler.ApplyContributorTag(player);
            };

            PlayerEvents.Left += ev =>
            {
                On(_left, new PlayerLeftEventArgs(ev.Player));
                
                if (!string.IsNullOrEmpty(ev.Player.UserId))
                    VerifiedPlayersCache.Verified.Remove(ev.Player.UserId);
            };

            PlayerEvents.Spawning += ev =>
                On(_spawning, new PlayerSpawningEventArgs(ev.Player));

            PlayerEvents.Spawned += ev =>
            {
                var player = ev.Player;
                On(_spawned, new PlayerSpawnedEventArgs(player));
            }; 

            PlayerEvents.Dying += ev =>
                On(_dying, new PlayerDyingEventArgs(ev.Player, ev.Attacker, ev.DamageHandler.ToString()));
            PlayerEvents.Hurting += ev =>
                On(_hurting, new PlayerHurtingEventArgs(ev.Player, ev.Attacker, ev.DamageHandler.GetHashCode()));

            PlayerEvents.ChangingRole += ev =>
                On(_changingRole, new PlayerChangingRoleEventArgs(ev.Player, ev.OldRole, ev.NewRole));

            PlayerEvents.ChangedRole += ev =>
                On(_changedRole, new PlayerChangedRoleEventArgs(
                    ev.Player.ReferenceHub,
                    ev.OldRole,
                    ev.NewRole,
                    ev.ChangeReason,
                    ev.SpawnFlags));

            PlayerEvents.PickingUpItem += ev =>
                On(_pickingUpItem, new PlayerPickingUpItemEventArgs(ev.Player.ReferenceHub, ev.Pickup.Base));

            PlayerEvents.DroppingItem += ev =>
                On(_droppingItem, new PlayerDroppingItemEventArgs(ev.Player.ReferenceHub, ev.Item.Base, ev.Throw));

            PlayerEvents.UsingItem += ev =>
                On(_usingItem, new PlayerUsingItemEventArgs(ev.Player.ReferenceHub, ev.UsableItem.Base));

            PlayerEvents.InteractingDoor += ev =>
                On(_interactingDoor, new PlayerInteractingDoorEventArgs(ev.Player.ReferenceHub, ev.Door.Base, ev.IsAllowed));

            PlayerEvents.InteractingElevator += ev =>
                On(_interactingElevator, new PlayerInteractingElevatorEventArgs(ev.Player.ReferenceHub, ev.Elevator.Base, ev.Panel));
            
            PlayerEvents.Banned += ev =>
                On(_banned, new PlayerBannedEventArgs(ev.Player, ev.Reason, ev.Duration));
            
            PlayerEvents.Kicked += ev =>
                On(_kicked, new PlayerKickedEventArgs(ev.Player, ev.Reason));
            
            Scp914Events.ProcessedPlayer += ev =>
                On(_upgrading, new UpgradingPlayersEventArgs(ev.Player, ev.KnobSetting));

            Log.Success("[PurgaLib] PlayerHandler registered on LabAPI.");
        }
    }
}
