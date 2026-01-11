using LabApi.Features.Wrappers;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;

public class ElevatorUsingEventArgs : System.EventArgs
{
    public LabApi.Features.Wrappers.Player Player { get; }
    public Elevator Elevator { get; }
    public bool IsAllowed { get; set; } = true;

    public ElevatorUsingEventArgs(LabApi.Features.Wrappers.Player player, Elevator elevator)
    {
        Player = player;
        Elevator = elevator;
    }
}