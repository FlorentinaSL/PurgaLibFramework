using System;
using LabApi.Events.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class DoorHandler
    {
        private static EventHandler<DoorInteractingEventArgs> _interacting;

        public static event EventHandler<DoorInteractingEventArgs> Interacting
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

        private static void OnInteracting(DoorInteractingEventArgs ev)
        {
            _interacting?.Invoke(null, ev);
        }

        public static void RegisterLabApi()
        {
            PlayerEvents.InteractingDoor += ev =>
            {
                var args = new DoorInteractingEventArgs(ev.Player, ev.Door)
                {
                    IsAllowed =  true
                };
                OnInteracting(args);
                
                if (!args.IsAllowed) 
                    ev.IsAllowed = false;
                
            };
            Log.Success("[PurgaLib] DoorHandler registered on LabApi.");
        }
    }
}