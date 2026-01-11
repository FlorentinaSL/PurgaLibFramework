using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public sealed class RoundActor : PActor
    {
        public RoundActor() { }

        public bool IsStarted { get; private set; } = false;

        public void Start()
        {
            PurgaLibEvent.Events.Handler.RoundHandler.OnStarting(new RoundStartingEventArgs());
            LabApi.Features.Wrappers.Round.Start();
            IsStarted = true;
            PurgaLibEvent.Events.Handler.RoundHandler.OnStarted(new RoundStartedEventArgs());
        }

        public void Restart()
        {
            PurgaLibEvent.Events.Handler.RoundHandler.OnRestarting(new RoundRestartingEventArgs());
            LabApi.Features.Wrappers.Round.Restart();
            IsStarted = true;
            PurgaLibEvent.Events.Handler.RoundHandler.OnStarted(new RoundStartedEventArgs());
        }

        public void Stop()
        {
            LabApi.Features.Wrappers.Round.End();
            IsStarted = false;
            PurgaLibEvent.Events.Handler.RoundHandler.OnEnded(new RoundEndedEventArgs("Unknown"));
        }

        public override bool IsAlive => true;
        public override UnityEngine.Transform Transform => null;

        protected override void Tick()
        {
            base.Tick();
        }
    }

    public static class Round
    {
        private static readonly RoundActor Core = StaticActor.Get<RoundActor>();

        public static bool IsStarted => Core.IsStarted;

        public static void Start() => Core.Start();

        public static void Restart() => Core.Restart();

        public static void Stop() => Core.Stop();
    }
}
