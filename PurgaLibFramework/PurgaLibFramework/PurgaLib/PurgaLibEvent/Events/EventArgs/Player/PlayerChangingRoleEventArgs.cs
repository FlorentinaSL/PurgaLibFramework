using PlayerRoles;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;

public class PlayerChangingRoleEventArgs : System.EventArgs
{
    public PurgaLibAPI.Features.Player Player { get; }
    public RoleTypeId OldRole { get; }
    public RoleTypeId NewRole { get; }

    public PlayerChangingRoleEventArgs(PurgaLibAPI.Features.Player player, RoleTypeId oldRole, RoleTypeId newRole)
    {
        Player = player;
        OldRole = oldRole;
        NewRole = newRole;
    }
}