using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MapGeneration;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class Room
    {
        public RoomIdentifier Base { get; }

        public Transform Transform => Base.transform;

        public GameObject GameObject => Base.gameObject;

        public Vector3 Position => Transform.position;

        public FacilityZone Zone => Base.Zone;
        
        private Room(RoomIdentifier room)
        {
            Base = room;
        }
        
        private static List<Room> _cache;

        public static IReadOnlyList<Room> List =>
            _cache ??= RoomIdentifier.AllRoomIdentifiers
                .Select(r => new Room(r))
                .ToList();

        public static void Refresh()
        {
            _cache = null;
        }
        
        public static Room From(RoomIdentifier identifier)
        {
            return new Room(identifier);
        }

        public static Room Random()
        {
            if (List.Count == 0) return null;
            int index = UnityEngine.Random.Range(0, List.Count);
            return List[index];
        }


        public static IEnumerable<Room> InZone(FacilityZone zone)
        {
            return List.Where(r => r.Zone == zone);
        }
        
        public float Distance(Vector3 point)
        {
            return Vector3.Distance(Position, point);
        }

        public bool Contains(Vector3 point, float radius = 10f)
        {
            return Distance(point) <= radius;
        }

        public Vector3 GetRandomPoint(float radius = 3f)
        {
            return Position + new Vector3(
                UnityEngine.Random.Range(-radius, radius),
                1f,
                UnityEngine.Random.Range(-radius, radius)
            );
        }

        
        public override string ToString()
        {
            return $"Room(Zone={Zone}, Pos={Position})";
        }
    }
}
