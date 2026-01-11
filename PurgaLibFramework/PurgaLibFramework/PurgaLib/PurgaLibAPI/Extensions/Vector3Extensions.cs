using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
        public static Vector3 Offset(this Vector3 v, float dx, float dy, float dz) => new Vector3(v.x + dx, v.y + dy, v.z + dz);
        public static Vector3 Offset(this Vector3 v, Vector3 offset) => v + offset;
    }
}
