using PurgaLibEvents.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class TeslaHandler
    {
        public static Event<OnInteractingTeslaEventArgs> InteractingTesla { get; set; } = new();
        internal static void OnInteractingTesla(OnInteractingTeslaEventArgs ev) => InteractingTesla?.Invoke(ev);
    }
}
