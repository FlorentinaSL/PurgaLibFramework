using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public static class DoorExtensions
    {
        public static void LockTemporarily(this Door door, float duration)
        {
            if (door == null) return;
            door.Base.IsLocked = true;
            MEC.Timing.CallDelayed(duration, () => door.Base.IsLocked = false);
        }

        public static void ToggleLock(this Door door)
        {
            if (door == null) return;
            door.Base.IsLocked = !door.Base.IsLocked;
        }

        public static bool IsOpen(this Door door)
        {
            return door != null && door.Base.IsOpened;
        }
    }
}
