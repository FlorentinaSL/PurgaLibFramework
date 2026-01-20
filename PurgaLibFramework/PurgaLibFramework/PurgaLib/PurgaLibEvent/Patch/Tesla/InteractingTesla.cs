using HarmonyLib;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Map;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Patch.Tesla
{
    [EventPatch(typeof(TeslaHandler), nameof(TeslaHandler.OnInteractingTesla))]
    [HarmonyPatch(typeof(TeslaGateController), nameof(TeslaGateController.FixedUpdate))]
    public static class TeslaGateControllerPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ReferenceHub __instance)
        {
            PurgaLibAPI.Features.Player p = new PurgaLibAPI.Features.Player(__instance);
            TeslaHandler.OnInteractingTesla(new OnInteractingTeslaEventArgs(p, true));
        }
    }
}
