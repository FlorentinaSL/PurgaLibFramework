using Scp914;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;

public class UpgradingPlayersEventArgs : System.EventArgs
{
    public LabApi.Features.Wrappers.Player Player { get; }
    public Scp914KnobSetting Setting { get;}

    public UpgradingPlayersEventArgs(LabApi.Features.Wrappers.Player player, Scp914KnobSetting setting)
    {
        Player = player;
        Setting = setting;
    }
    
}