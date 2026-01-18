using GameCore;
using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Round;

[EventPatch(typeof(RoundHandler), nameof(RoundHandler.OnStarted))]
[EventPatch(typeof(RoundHandler), nameof(RoundHandler.OnStarted))]
[HarmonyPatch(typeof(RoundStart), nameof(RoundStart.RoundStarted))]
public static class RoundStarted
{
    [HarmonyPrefix]
    private static void Prefix(bool value)
    {
        if (!value || value == RoundStart.RoundStarted)
            return;
        RoundHandler.OnStarting(new RoundStartingEventArgs());
        Log.Success("Starting Invoked!");
    }

    [HarmonyPostfix]
    private static void Postfix(bool value)
    {
         if (!value || value == RoundStart.RoundStarted)
            return;
         RoundHandler.OnStarted(new RoundStartedEventArgs());
         Log.Success("Starting Invoked!");
    }
}