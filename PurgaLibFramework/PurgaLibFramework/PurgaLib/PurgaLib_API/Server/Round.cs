using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Server;

public static class Round
{
    public static bool IsStarted { get; private set; } = false;
    
    public static void Start()
    {
        PurgaLibEvent.Events.Handler.Round.OnStarting(new RoundStartingEventArgs());

        LabApi.Features.Wrappers.Round.Start();

        IsStarted = true;
        
        PurgaLibEvent.Events.Handler.Round.OnStarted(new RoundStartedEventArgs());
    }
    
    public static void Restart()
    {
        PurgaLibEvent.Events.Handler.Round.OnRestarting(new RoundRestartingEventArgs());
        
        LabApi.Features.Wrappers.Round.Restart();
        
        IsStarted = true;
        
        PurgaLibEvent.Events.Handler.Round.OnStarted(new RoundStartedEventArgs());
    }
    
    public static void Stop()
    {
        LabApi.Features.Wrappers.Round.End();
        
        IsStarted = false;
        
        PurgaLibEvent.Events.Handler.Round.OnEnded(new RoundEndedEventArgs("Unknown"));
    }
}