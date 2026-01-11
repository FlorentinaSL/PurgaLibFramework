namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core
{
    public abstract class PComponent : PObject
    {
        public PActor Owner { get; internal set; }

        protected virtual void OnAttached() { }
        protected virtual void OnDetached() { }

        internal void Attach(PActor actor)
        {
            Owner = actor;
            OnAttached();
        }

        internal void Detach()
        {
            OnDetached();
            Owner = null;
        }
    }
}
