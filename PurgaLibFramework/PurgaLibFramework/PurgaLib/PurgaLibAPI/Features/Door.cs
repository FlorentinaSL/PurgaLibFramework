using System.Collections.Generic;
using System.Linq;
using Interactables.Interobjects.DoorUtils;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.DoorVariantMapper;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public sealed class Door
    {
        private readonly DoorVariant _door;
        private static readonly Dictionary<DoorVariant, Door> Cache = new();

        public Door(DoorVariant door)
        {
            _door = door;
            Type = DoorMapper.GetDoorType(door); 
        }

        public DoorVariant Base => _door;
        
        public DoorType Type { get; private set; } = DoorType.UnknownDoor;

        public Vector3 Position => _door.gameObject.transform.position;
        public string Name => _door.gameObject.name;

        public bool IsOpen
        {
            get => _door.NetworkTargetState;
            set => _door.NetworkTargetState = value;
        }

        public bool IsLocked
        {
            get => DoorLockType != DoorLockType.None;
            set
            {
                if (value) Lock(DoorLockType.None);
                else Unlock();
            }
        }

        public DoorLockType DoorLockType
        {
            get => (DoorLockType)_door.NetworkActiveLocks;
            set => ChangeLock(value);
        }

        public void ChangeLock(DoorLockType lockType)
        {
            if (lockType == DoorLockType.None)
                _door.NetworkActiveLocks = 0;
            else
            {
                var locks = DoorLockType;
                if (locks.HasFlag(lockType))
                    locks &= ~lockType;
                else
                    locks |= lockType;

                _door.NetworkActiveLocks = (ushort)locks;
            }

            DoorEvents.TriggerAction(_door, IsLocked ? DoorAction.Locked : DoorAction.Unlocked, null);
        }

        public void Lock(DoorLockType lockType)
        {
            var locks = DoorLockType;
            locks |= lockType;
            _door.NetworkActiveLocks = (ushort)locks;

            DoorEvents.TriggerAction(_door, IsLocked ? DoorAction.Locked : DoorAction.Unlocked, null);
        }

        public void Lock(float time, DoorLockType lockType)
        {
            Lock(lockType);
            Unlock(time, lockType);
        }

        public void Unlock() => ChangeLock(DoorLockType.None);

        public void Unlock(float time, DoorLockType flagsToUnlock)
        {
            DoorScheduledUnlocker.UnlockLater(_door, time, (DoorLockReason)flagsToUnlock);
        }

        public float ExactState => _door.GetExactState();

        public bool IsFullyOpen => ExactState >= 1f;
        public bool IsFullyClosed => ExactState <= 0f;

        public static IReadOnlyCollection<Door> List =>
            DoorVariant.AllDoors.Select(Get).Where(d => d != null).ToList();

        public static Door Get(DoorVariant variant)
        {
            if (variant == null) return null;

            if (!Cache.TryGetValue(variant, out var door))
            {
                door = new Door(variant);
                Cache.Add(variant, door);
            }
            return door;
        }

        public static Door GetClosest(Vector3 pos, out float distance)
        {
            Door best = null;
            float bestDist = float.MaxValue;

            foreach (var variant in DoorVariant.AllDoors)
            {
                float d = Vector3.Distance(pos, variant.gameObject.transform.position);
                if (d < bestDist)
                {
                    bestDist = d;
                    best = Get(variant);
                }
            }

            distance = bestDist;
            return best;
        }

        public override string ToString() => $"{Name} ({Position}) - Type: {Type}";
    }
}
