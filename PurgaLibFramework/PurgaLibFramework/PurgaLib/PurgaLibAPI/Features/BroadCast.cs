namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class PlayerBroadcast
    {
        public PlayerBroadcast(string content, ushort duration = 10, bool show = true, Broadcast.BroadcastFlags type = Broadcast.BroadcastFlags.Normal)
        {
            Content = content;
            Duration = duration;
            Show = show;
            Type = type;
        }

        public string Content { get; } 
        public ushort Duration { get; } 
        public bool Show { get; }       
        public Broadcast.BroadcastFlags Type { get; } 
    }
}
