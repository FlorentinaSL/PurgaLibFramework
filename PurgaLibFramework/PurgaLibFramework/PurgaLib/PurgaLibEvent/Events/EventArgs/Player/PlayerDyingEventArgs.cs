namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player
{
    public class PlayerDyingEventArgs : System.EventArgs
    {
        public global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player Player { get; }
        public global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player Killer { get; }
        public string Reason { get; }
        public bool IsAllowed { get; set; } = true;

        public PlayerDyingEventArgs(global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player player, global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player killer, string reason)
        {
            Player = player;
            Killer = killer;
            Reason = reason;
        }
    }
}