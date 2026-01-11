using System;
using LabApi.Events.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class ElevatorHandler
    {
        private static EventHandler<ElevatorUsingEventArgs> _interacting;

        public static event EventHandler<ElevatorUsingEventArgs> Interacting
        {
            add
            {
                bool wasEmpty = _interacting == null;
                _interacting += value;
                if (wasEmpty)
                    RegisterLabApi();
            }
            remove
            {
                _interacting -= value;
            }
        }

        private static void OnInteracting(ElevatorUsingEventArgs ev)
        {
            _interacting?.Invoke(null, ev);
        }

        public static void RegisterLabApi()
        {
            PlayerEvents.InteractingElevator += ev =>
            {
                var args = new ElevatorUsingEventArgs(ev.Player, ev.Elevator)
                {
                    IsAllowed = true
                };
                OnInteracting(args);
                
                if(!args.IsAllowed)
                    ev.IsAllowed = false;
            
            };
            
            Log.Success("[PurgaLib] ElevatorHandler registered on LabApi.");
        }
    }
}