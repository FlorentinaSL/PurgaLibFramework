using MEC;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core.Interfaces;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core
{
    public abstract class PActor : PObject, IEntity, IWorldObject
    {
        public const float DefaultTickRate = 0.1f;

        private CoroutineHandle tickHandle;
        private bool canTick = true;
        private float tickRate = DefaultTickRate;

        protected PActor()
        {
            PostInitialize();
            Timing.CallDelayed(tickRate, BeginPlay);
            Timing.CallDelayed(tickRate * 2f, StartTick);
        }

        public abstract Transform Transform { get; }
        public abstract bool IsAlive { get; }

        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }

        public bool CanTick
        {
            get => canTick;
            set
            {
                canTick = value;
                if (canTick) Timing.ResumeCoroutines(tickHandle);
                else Timing.PauseCoroutines(tickHandle);
            }
        }

        public float TickRate
        {
            get => tickRate;
            set => tickRate = Mathf.Max(0.01f, value);
        }

        private void StartTick()
        {
            tickHandle = Timing.RunCoroutine(TickLoop());
        }

        private System.Collections.Generic.IEnumerator<float> TickLoop()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(TickRate);
                Tick();
            }
        }

        protected virtual void PostInitialize() { }
        protected virtual void BeginPlay() { SubscribeEvents(); }
        protected virtual void Tick() { }
        protected virtual void EndPlay() { UnsubscribeEvents(); }

        protected virtual void SubscribeEvents() { }
        protected virtual void UnsubscribeEvents() { }

        protected override void OnDestroy()
        {
            Timing.KillCoroutines(tickHandle);
            EndPlay();
        }
    }
}
