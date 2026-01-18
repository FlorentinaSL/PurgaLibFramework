using HarmonyLib;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Round
{
    [EventPatch(typeof(RoundHandler), nameof(RoundHandler.OnEnded))]
    [HarmonyPatch(typeof(RoundSummary), nameof(RoundSummary.IsRoundEnded), MethodType.Setter)]
    public static class RoundEndedPatch
    {
        [HarmonyPrefix]
        private static void Prefix(Team winner)
        {
            RoundHandler.OnEnded(new RoundEndedEventArgs(winner.ToString()));
        }
    }
}
