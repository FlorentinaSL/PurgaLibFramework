using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Player;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibExample.Event;

public class PlayerVer
{
    private void PlayerVeri(PlayerVerifiedEventArgs ev)
    {
        Log.Success($"Player successfully entered: {ev.Player.Nickname}");
        ev.Player.SendBroadcast(Example.Instance.Config.Message, 6);
    }

    public void Register()
    {
        PlayerHandler.Verified += PlayerVeri;
    }

    public void UnRegister()
    {
        PlayerHandler.Verified -= PlayerVeri;
    }
}