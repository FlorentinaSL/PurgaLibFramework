using PurgaLibEvents.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class ElevatorHandler
    {
        public static Event<ElevatorUsingEventArgs> Interacting { get; set; } = new();
        internal static void OnInteracting(ElevatorUsingEventArgs ev) => Interacting?.Invoke(ev);
    }
}
