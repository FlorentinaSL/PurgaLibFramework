using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public sealed class MapActor : PActor
    {
        public void TurnOffAllLights(ushort duration)
        {
            LabApi.Features.Wrappers.Map.TurnOffLights(duration);
        }

        public void ResetAllLights()
        {
            LabApi.Features.Wrappers.Map.ResetColorOfLights();
        }

        public void SetColorOfAllLights(Color color)
        {
            LabApi.Features.Wrappers.Map.SetColorOfLights(color);
        }

        public void TurnOnAllLights()
        {
            LabApi.Features.Wrappers.Map.TurnOnLights();
        }

        public override bool IsAlive => true;
        public override Transform Transform => null;

        protected override void Tick() { }
    }

    public static class Map
    {
        private static readonly MapActor Core = StaticActor.Get<MapActor>();

        public static void TurnOffAllLights(ushort duration) => Core.TurnOffAllLights(duration);

        public static void ResetAllLights() => Core.ResetAllLights();

        public static void SetColorOfAllLights(Color color) => Core.SetColorOfAllLights(color);

        public static void TurnOnAllLights() => Core.TurnOnAllLights();
    }
}
