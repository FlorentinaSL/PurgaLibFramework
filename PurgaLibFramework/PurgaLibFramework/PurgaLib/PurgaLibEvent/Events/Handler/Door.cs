using PurgaLibEvents.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class DoorHandler
    {
        public static Event<DoorInteractingEventArgs> Interacting { get; set; } = new();
        
        internal static void OnInteracting(DoorInteractingEventArgs ev) => Interacting?.Invoke(ev);
    }
}
