namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map
{
    public class OnInteractingTeslaEventArgs : System.EventArgs
    {
        public LabApi.Features.Wrappers.Player Player { get; }
        public bool IsAllowed { get; set; }

        public OnInteractingTeslaEventArgs(LabApi.Features.Wrappers.Player player, bool isAllowed = true)
        {
            Player = player ?? throw new System.ArgumentNullException(nameof(player));
            IsAllowed = isAllowed;
        }
    }
}