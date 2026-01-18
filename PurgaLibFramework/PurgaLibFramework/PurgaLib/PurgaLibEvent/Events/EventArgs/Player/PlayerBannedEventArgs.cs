namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;

public class PlayerBannedEventArgs : System.EventArgs
{
    public global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player Player { get; }
    public string Reason { get; }
    public long Duration { get; }

    public PlayerBannedEventArgs(global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player player, string reason, long duration)
    {
        Player = player;
        Duration = duration;
        Reason = reason;
    }
}