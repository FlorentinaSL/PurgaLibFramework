using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class Door
    {
        public static event EventHandler<DoorInteractingEventArgs> Interacting;

        internal static void OnInteracting(DoorInteractingEventArgs ev)
        {
            EventManager.Invoke(Interacting, null, ev);
        }
    }
}