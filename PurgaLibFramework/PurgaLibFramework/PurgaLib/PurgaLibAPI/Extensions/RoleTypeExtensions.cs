using PlayerRoles;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class RoleTypeExtensions
    {
        public static bool IsSCP(this RoleTypeId role)
        {
            return role == RoleTypeId.Scp049 ||
                   role == RoleTypeId.Scp096 ||
                   role == RoleTypeId.Scp106 ||
                   role == RoleTypeId.Scp173 ||
                   role == RoleTypeId.Scp079 ||
                   role == RoleTypeId.Scp939;
        }

        public static bool IsHuman(this RoleTypeId role)
        {
            return !role.IsSCP() && role != RoleTypeId.Spectator;
        }
    }
}
