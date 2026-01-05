using System;
using LabApi.Events.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class TeslaHandler
    {
        private static EventHandler<OnInteractingTeslaEventArgs> _interacting;

        public static event EventHandler<OnInteractingTeslaEventArgs> Interacting
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

        private static void OnInteracting(OnInteractingTeslaEventArgs args)
        {
            _interacting?.Invoke(null, args);
        }

        public static void RegisterLabApi()
        {
            PlayerEvents.TriggeredTesla += ev =>
            {
                var args = new OnInteractingTeslaEventArgs(ev.Player)
                {
                    IsEnabled = true
                };
                OnInteracting(args);
            };

            Log.Success("[PurgaLib] TeslaHandler registered on LabApi.");
        }
    }
}