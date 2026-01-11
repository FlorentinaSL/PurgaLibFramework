namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core
{
    public abstract class PObject
    {
        public string Name { get; protected set; }
        public bool IsDestroyed { get; private set; }

        protected PObject()
        {
            Name = GetType().Name;
        }

        protected virtual void OnDestroy() { }

        internal void DestroyInternal()
        {
            if (IsDestroyed)
                return;

            IsDestroyed = true;
            OnDestroy();
        }

        public override string ToString()
            => $"{GetType().Name} ({Name})";
    }
}
