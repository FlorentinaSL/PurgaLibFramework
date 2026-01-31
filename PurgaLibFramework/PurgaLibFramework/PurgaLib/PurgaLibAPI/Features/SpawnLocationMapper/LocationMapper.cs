using System.Linq;
using MapGeneration;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.SpawnLocationMapper
{
    public static class LocationMapper
    {
        public static Vector3 GetPoint(SpawnLocationType type)
        {
            Room room = null;

            switch (type)
            {
                case SpawnLocationType.Random:
                    room = Room.Random();
                    break;

                case SpawnLocationType.Surface:
                    room = Room.InZone(FacilityZone.Surface)
                               .OrderBy(r => UnityEngine.Random.value)
                               .FirstOrDefault();
                    break;

                case SpawnLocationType.Entrance:
                    room = Room.InZone(FacilityZone.Entrance)
                               .OrderBy(r => UnityEngine.Random.value)
                               .FirstOrDefault();
                    break;

                case SpawnLocationType.LightContainment:
                    room = Room.InZone(FacilityZone.LightContainment)
                               .OrderBy(r => UnityEngine.Random.value)
                               .FirstOrDefault();
                    break;

                case SpawnLocationType.HeavyContainment:
                    room = Room.InZone(FacilityZone.HeavyContainment)
                               .OrderBy(r => UnityEngine.Random.value)
                               .FirstOrDefault();
                    break;
                
                case SpawnLocationType.PocketDimension:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("Pocket"));
                    break;

                case SpawnLocationType.Tutorial:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("Tutorial"));
                    break;

                case SpawnLocationType.MtfSpawn:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("MTF"));
                    break;

                case SpawnLocationType.ChaosSpawn:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("Chaos"));
                    break;

                case SpawnLocationType.FacilityGuard:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("Guard"));
                    break;

                case SpawnLocationType.ScientistSpawn:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("Scientist"));
                    break;

                case SpawnLocationType.ClassDSpawn:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("ClassD"));
                    break;

                case SpawnLocationType.Scp079Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("079"));
                    break;

                case SpawnLocationType.Scp173Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("173"));
                    break;

                case SpawnLocationType.Scp049Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("049"));
                    break;

                case SpawnLocationType.Scp096Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("096"));
                    break;

                case SpawnLocationType.Scp106Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("106"));
                    break;

                case SpawnLocationType.Scp939Room:
                    room = Room.List.FirstOrDefault(r => r.Base.gameObject.name.Contains("939"));
                    break;

                case SpawnLocationType.None:
                default:
                    return Vector3.zero;
            }

            return room != null ? room.GetRandomPoint() : Vector3.zero;
        }
    }
}
