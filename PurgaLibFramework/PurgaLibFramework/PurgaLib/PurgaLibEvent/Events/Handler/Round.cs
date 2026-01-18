using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler
{
    public static class RoundHandler
    {

        public static Event<RoundStartingEventArgs> Starting { get; set; } = new();
        public static Event<RoundStartedEventArgs> Started { get; set; } = new();
        public static Event<RoundEndedEventArgs> Ended { get; set; } = new();
        public static Event<RoundRestartingEventArgs> Restarting { get; set; } = new();
        
        internal static void OnStarting(RoundStartingEventArgs ev) => Starting?.Invoke(ev);
        internal static void OnStarted(RoundStartedEventArgs ev) => Started?.Invoke(ev);
        internal static void OnEnded(RoundEndedEventArgs ev) => Ended?.Invoke(ev);
        internal static void OnRestarting(RoundRestartingEventArgs ev) => Restarting?.Invoke(ev);
    }
}
