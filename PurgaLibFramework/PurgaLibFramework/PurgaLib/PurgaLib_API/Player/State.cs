using PlayerRoles;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Player
{
    public static class State
    {
        public static string Name(LabApi.Features.Wrappers.Player player)
        {
            return player.Nickname;
        }

        public static int Id(LabApi.Features.Wrappers.Player player)
        {
            return player.PlayerId;
        }

        public static RoleTypeId Role(LabApi.Features.Wrappers.Player player)
        {
            return player.Role;
        }

        public static float Health(LabApi.Features.Wrappers.Player player)
        {
            return player.Health;
        }

        public static (float x, float y, float z) Position(LabApi.Features.Wrappers.Player player)
        {
            var pos = player.Position;
            return (pos.x, pos.y, pos.z);
        }
    }
}