using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class Elevator
    {
        public static event EventHandler<ElevatorUsingEventArgs> Interacting;

        internal static void OnInteracting(ElevatorUsingEventArgs ev)
        {
            EventManager.Invoke(Interacting, null, ev);
        }
    }
}