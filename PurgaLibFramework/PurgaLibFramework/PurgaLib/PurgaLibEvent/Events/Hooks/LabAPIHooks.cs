using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;
using Player = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.Player;
using PlayerChangingRoleEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerChangingRoleEventArgs;
using PlayerDyingEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerDyingEventArgs;
using PlayerJoinedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerJoinedEventArgs;
using PlayerLeftEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerLeftEventArgs;
using PlayerSpawnedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawnedEventArgs;
using PlayerSpawningEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player.PlayerSpawningEventArgs;
using HandlerRound = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.Round;
using RoundEndedEventArgs = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round.RoundEndedEventArgs;
using HandlerDoor = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.Door;
using HandleElevator = PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.Elevator;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Hooks
{
    public static class LabHook
    {
        public static void Register()
        {
            PlayerEvents.Joined += ev => Player.OnJoined(new PlayerJoinedEventArgs(ev.Player));
            PlayerEvents.Left += ev => Player.OnLeft(new PlayerLeftEventArgs(ev.Player));
            PlayerEvents.Spawning += ev => Player.OnSpawning(new PlayerSpawningEventArgs(ev.Player));
            PlayerEvents.Spawned += ev => Player.OnSpawned(new PlayerSpawnedEventArgs(ev.Player));
            
            PlayerEvents.Dying += ev =>
            {
                var args = new PlayerDyingEventArgs(ev.Player, ev.Attacker, ev.DamageHandler.ToString());
                Player.OnDying(args);
                if (!args.IsAllowed) ev.IsAllowed = false;
            };
            
            PlayerEvents.ChangingRole += ev =>
            {
                var args = new PlayerChangingRoleEventArgs(ev.Player, ev.NewRole, ev.OldRole);
                Player.OnChangingRole(args);
                if (!args.IsAllowed) ev.IsAllowed = false;
            };
            
            PlayerEvents.ChangedRole += ev => Player.OnChangedRole(new PlayerChangedRoleEventArgs(
                ev.Player.ReferenceHub,
                ev.OldRole,
                ev.NewRole,
                ev.ChangeReason,
                ev.SpawnFlags
            ));

            HandlerRound.WaitingForPlayers += (_, __) => HandlerRound.OnWaitingForPlayers(new WaitingForPlayersEventArgs());
            HandlerRound.Starting += (_, __) => HandlerRound.OnStarting(new RoundStartingEventArgs());
            HandlerRound.Started += (_, __) => HandlerRound.OnStarted(new RoundStartedEventArgs());
            HandlerRound.Ended += (sender, ev) => HandlerRound.OnEnded(new RoundEndedEventArgs(ev.LeadingTeam.ToString()));
            HandlerRound.Restarting += (_, __) => HandlerRound.OnRestarting(new RoundRestartingEventArgs());
            
            PlayerEvents.InteractingDoor += ev =>
            {
                var args = new DoorInteractingEventArgs(ev.Player, ev.Door);
                HandlerDoor.OnInteracting(args);
            };
            PlayerEvents.InteractingElevator += ev =>
            {
                var args = new ElevatorUsingEventArgs(ev.Player, ev.Elevator);
                HandleElevator.OnInteracting(args);
            };
        }
    }
}
