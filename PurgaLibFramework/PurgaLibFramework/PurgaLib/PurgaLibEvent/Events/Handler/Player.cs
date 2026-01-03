using System;
using LabApi.Events.Arguments.PlayerEvents;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PlayerChangingRoleEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerChangingRoleEventArgs;
using PlayerDyingEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerDyingEventArgs;
using PlayerHurtingEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerHurtingEventArgs;
using PlayerJoinedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerJoinedEventArgs;
using PlayerLeftEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerLeftEventArgs;
using PlayerSpawnedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawnedEventArgs;
using PlayerSpawningEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawningEventArgs;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class Player
    {
        public static event EventHandler<PlayerJoinedEventArgs> Joined;
        public static event EventHandler<PlayerLeftEventArgs> Left;
        
        public static event EventHandler<PlayerHurtingEventArgs> Hurting;
        public static event EventHandler<PlayerDyingEventArgs> Dying;
        public static event EventHandler<PlayerDiedEventArgs> Died;
        
        public static event EventHandler<PlayerChangingRoleEventArgs> ChangingRole;
        public static event EventHandler<PlayerChangedRoleEventArgs> ChangedRole;
        
        public static event EventHandler<PlayerSpawningEventArgs> Spawning;
        public static event EventHandler<PlayerSpawnedEventArgs> Spawned;
        
        public static event EventHandler<PlayerPickingUpItemEventArgs> PickingUpItem;
        public static event EventHandler<PlayerDroppingItemEventArgs> DroppingItem;
        public static event EventHandler<PlayerUsingItemEventArgs> UsingItem;
        
        public static event EventHandler<PlayerInteractingDoorEventArgs> InteractingDoor;
        public static event EventHandler<PlayerInteractingElevatorEventArgs> InteractingElevator;

        internal static void OnJoined(PlayerJoinedEventArgs ev)
            => EventManager.Invoke(Joined, null, ev);

        internal static void OnLeft(PlayerLeftEventArgs ev)
            => EventManager.Invoke(Left, null, ev);

        internal static void OnHurting(PlayerHurtingEventArgs ev)
            => EventManager.Invoke(Hurting, null, ev);

        internal static void OnDying(PlayerDyingEventArgs ev)
            => EventManager.Invoke(Dying, null, ev);

        internal static void OnDied(PlayerDiedEventArgs ev)
            => EventManager.Invoke(Died, null, ev);

        internal static void OnChangingRole(PlayerChangingRoleEventArgs ev)
            => EventManager.Invoke(ChangingRole, null, ev);

        internal static void OnChangedRole(PlayerChangedRoleEventArgs ev)
            => EventManager.Invoke(ChangedRole, null, ev);

        internal static void OnSpawning(PlayerSpawningEventArgs ev)
            => EventManager.Invoke(Spawning, null, ev);

        internal static void OnSpawned(PlayerSpawnedEventArgs ev)
            => EventManager.Invoke(Spawned, null, ev);

        internal static void OnPickingUpItem(PlayerPickingUpItemEventArgs ev)
            => EventManager.Invoke(PickingUpItem, null, ev);

        internal static void OnDroppingItem(PlayerDroppingItemEventArgs ev)
            => EventManager.Invoke(DroppingItem, null, ev);

        internal static void OnUsingItem(PlayerUsingItemEventArgs ev)
            => EventManager.Invoke(UsingItem, null, ev);

        internal static void OnInteractingDoor(PlayerInteractingDoorEventArgs ev)
            => EventManager.Invoke(InteractingDoor, null, ev);

        internal static void OnInteractingElevator(PlayerInteractingElevatorEventArgs ev)
            => EventManager.Invoke(InteractingElevator, null, ev);
    }
    
}
