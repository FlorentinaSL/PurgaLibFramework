using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;
using RoundRestarting;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Round;

[EventPatch(typeof(RoundHandler), nameof(RoundHandler.OnRestarting))]
[HarmonyPatch(typeof(RoundRestart), nameof(RoundRestart.IsRoundRestarting), MethodType.Setter)]
public static class RoundRestarting
{
    [HarmonyPrefix]
    private static void Prefix(bool value)
    {
        if (!value || value == RoundRestart.IsRoundRestarting)
            return;
        RoundHandler.OnRestarting(new RoundRestartingEventArgs());
    }
}