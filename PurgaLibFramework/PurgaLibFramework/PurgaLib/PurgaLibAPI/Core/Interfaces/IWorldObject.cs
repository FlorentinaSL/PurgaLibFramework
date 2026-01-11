using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core.Interfaces
{
    public interface IWorldObject
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }
}
