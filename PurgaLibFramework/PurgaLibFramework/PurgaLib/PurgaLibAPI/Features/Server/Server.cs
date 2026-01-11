using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public sealed class ServerActor : PActor
    {
        public ServerActor() { }

        public int PlayerCount => LabApi.Features.Wrappers.Player.List.Count;

        public void Restart() => LabApi.Features.Wrappers.Server.Restart();

        public void Stop() => LabApi.Features.Wrappers.Server.Shutdown();

        public void Broadcast(string message, ushort duration) =>
            LabApi.Features.Wrappers.Server.SendBroadcast(message, duration);

        public override Transform Transform { get; } = null;
        public override bool IsAlive { get; } = true;
        protected override void Tick()
        {
            base.Tick();
            // eventuali logiche periodiche del server
        }
    }

    public static class Server
    {
        private static readonly ServerActor Core = StaticActor.Get<ServerActor>();

        public static int PlayerCount => Core.PlayerCount;

        public static void Restart() => Core.Restart();

        public static void Stop() => Core.Stop();

        public static void Broadcast(string message, ushort duration) => Core.Broadcast(message, duration);
    }
}
