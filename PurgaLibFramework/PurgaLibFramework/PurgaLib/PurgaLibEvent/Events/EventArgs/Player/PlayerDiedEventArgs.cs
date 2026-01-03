using PluginAPI.Enums;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player
{
    public class PlayerDiedEventArgs : System.EventArgs
    {
        public LabApi.Features.Wrappers.Player Player { get; }
        public LabApi.Features.Wrappers.Player Killer { get; }
        public DamageType DamageType { get; }

        public PlayerDiedEventArgs(LabApi.Features.Wrappers.Player player, LabApi.Features.Wrappers.Player killer, DamageType damageType)
        {
            Player = player;
            Killer = killer;
            DamageType = damageType;
        }
    }
}