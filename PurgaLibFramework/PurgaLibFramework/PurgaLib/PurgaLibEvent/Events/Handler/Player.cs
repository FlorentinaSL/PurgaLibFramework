using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.EventsArgs;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class PlayerHandler
    {
        public static Event<PlayerJoinedEventArgs> Joined { get; set; } = new(); 
        public static Event<PlayerLeftEventArgs> Left { get; set; } = new(); 
        public static Event<PlayerVerifiedEventArgs> Verified { get; set; } = new(); 
        public static Event<PlayerHurtingEventArgs> Hurting { get; set; } = new(); 
        public static Event<PlayerDyingEventArgs> Dying { get; set; } = new();
        public static Event<PlayerDiedEventArgs> Died { get; set; } = new();
        public static Event<PlayerChangingRoleEventArgs> ChangingRole { get; set; } = new();
        public static Event<PlayerChangedRoleEventArgs> ChangedRole { get; set; } = new();
        public static Event<PlayerSpawningEventArgs> Spawning { get; set; } = new(); 
        public static Event<PlayerSpawnedEventArgs> Spawned { get; set; } = new();
        public static Event<PlayerBannedEventArgs> Banned { get; set; } = new();
        public static Event<PlayerKickedEventArgs> Kicked { get; set; } = new();
        public static Event<UpgradingPlayersEventArgs> Upgrading { get; set; } = new();
        public static Event<CustomItemPickedUpEventArgs> ItemPickedUp { get; set; } = new();

        internal static void OnJoined(PlayerJoinedEventArgs ev) => Joined?.Invoke(ev);
        internal static void OnLeft(PlayerLeftEventArgs ev) => Left?.Invoke(ev);
        internal static void OnVerified(PlayerVerifiedEventArgs ev) => Verified?.Invoke(ev);
        internal static void OnHurting(PlayerHurtingEventArgs ev) => Hurting?.Invoke(ev);
        internal static void OnDying(PlayerDyingEventArgs ev) => Dying?.Invoke(ev);
        internal static void OnDied(PlayerDiedEventArgs ev) => Died?.Invoke(ev);
        internal static void OnChangingRole(PlayerChangingRoleEventArgs ev) => ChangingRole?.Invoke(ev);
        internal static void OnChangedRole(PlayerChangedRoleEventArgs ev) => ChangedRole?.Invoke(ev);
        internal static void OnSpawning(PlayerSpawningEventArgs ev) => Spawning?.Invoke(ev);
        internal static void OnSpawned(PlayerSpawnedEventArgs ev) => Spawned?.Invoke(ev);
        internal static void OnBanned(PlayerBannedEventArgs ev) => Banned?.Invoke(ev);
        internal static void OnKicked(PlayerKickedEventArgs ev) => Kicked?.Invoke(ev);
        internal static void OnUpgrading(UpgradingPlayersEventArgs ev) => Upgrading?.Invoke(ev);
        internal static void OnItemPickedUp(CustomItemPickedUpEventArgs ev) => ItemPickedUp?.Invoke(ev);
    }
}
