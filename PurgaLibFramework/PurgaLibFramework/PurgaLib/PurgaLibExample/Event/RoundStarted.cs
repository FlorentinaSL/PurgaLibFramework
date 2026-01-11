using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibExample.Event;

public class RoundStarted
{
    private void RoundStart(object sender, RoundStartingEventArgs ev)
    {
        Server.Broadcast(Example.Instance.Config.Message, 5);
    }

    public void Register()
    {
        PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.RoundHandler.Starting += RoundStart;
    }

    public void UnRegister()
    {
        PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler.RoundHandler.Starting -= RoundStart;
    }
}
