using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class ServerExtensions
    {
        public static bool HasPlayers(this ServerActor server)
        {
            return server.PlayerCount > 0;
        }

        public static void BroadcastFormatted(this ServerActor server, string message, ushort duration, string prefix = "[Server] ")
        {
            server.Broadcast($"{prefix}{message}", duration);
        }
    }
}
