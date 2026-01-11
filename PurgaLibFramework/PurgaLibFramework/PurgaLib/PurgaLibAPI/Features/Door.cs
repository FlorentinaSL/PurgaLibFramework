using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public sealed class Door
    {
        internal LabApi.Features.Wrappers.Door Base { get; }

        private static readonly Dictionary<LabApi.Features.Wrappers.Door, Door> Cache = new();

        private Door(LabApi.Features.Wrappers.Door door)
        {
            Base = door;
        }

        public Vector3 Position => Base.Transform.position;
        public string Name => Base.GameObject.name;

        public bool IsLocked
        {
            get => Base.IsLocked;
            set => Base.IsLocked = value;
        }

        public bool IsOpen
        {
            get => Base.IsOpened;
            set => Base.IsOpened = value;
        }

        public void Open()
            => Base.IsOpened = true;

        public void Close()
            => Base.IsOpened = false;

        public void Lock()
            => Base.IsLocked = true;

        public void Unlock()
            => Base.IsLocked = false;

        public void Lock(float duration)
        {
            Base.IsLocked = true;
            Timing.CallDelayed(duration, () => Base.IsLocked = false);
        }

        public static IReadOnlyCollection<Door> List =>
            LabApi.Features.Wrappers.Door.List
                .Select(Get)
                .Where(d => d != null)
                .ToList();

        public static Door Get(LabApi.Features.Wrappers.Door door)
        {
            if (door == null)
                return null;

            if (!Cache.TryGetValue(door, out var wrapped))
            {
                wrapped = new Door(door);
                Cache.Add(door, wrapped);
            }

            return wrapped;
        }

        public static void LockAll(float duration)
        {
            foreach (var door in List)
                door.Lock(duration);
        }

        public static void UnlockAll()
        {
            foreach (var door in List)
                door.Unlock();
        }

        internal static void Remove(LabApi.Features.Wrappers.Door door)
            => Cache.Remove(door);
    }
}
