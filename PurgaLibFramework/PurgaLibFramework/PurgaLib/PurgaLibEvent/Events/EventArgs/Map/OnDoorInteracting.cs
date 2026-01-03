using LabApi.Features.Wrappers;
using System;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map
{
    public class DoorInteractingEventArgs : System.EventArgs
    {
        public LabApi.Features.Wrappers.Player Player { get; }
        public LabApi.Features.Wrappers.Door Door { get; }
        public bool IsAllowed { get; set; } = true;

        public DoorInteractingEventArgs(LabApi.Features.Wrappers.Player player, LabApi.Features.Wrappers.Door door)
        {
            Player = player;
            Door = door;
        }
    }
}