using System;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;

public class PlayerChangedRoleEventArgs : EventArgs
{
    public Player Player { get; }
    public RoleTypeId OldRole { get; }
    public RoleTypeId NewRole { get; }
    public RoleChangeReason Reason { get; }
    public RoleSpawnFlags SpawnFlags { get; }

    public PlayerChangedRoleEventArgs(
        Player player,
        RoleTypeId oldRole,
        RoleTypeId newRole,
        RoleChangeReason reason,
        RoleSpawnFlags spawnFlags)
    {
        Player = player;
        OldRole = oldRole;
        NewRole = newRole;
        Reason = reason;
        SpawnFlags = spawnFlags;
    }
}
