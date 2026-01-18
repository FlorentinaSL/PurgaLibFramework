namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map
{
    public class OnInteractingTeslaEventArgs : System.EventArgs
    {
        public global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player Player { get; }
        public bool IsAllowed { get; set; }

        public OnInteractingTeslaEventArgs(global::PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Player player, bool isAllowed = true)
        {
            Player = player ?? throw new System.ArgumentNullException(nameof(player));
            IsAllowed = isAllowed;
        }
    }
}