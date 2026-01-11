namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core
{
    public abstract class TickComponent : PComponent
    {
        public bool Enabled { get; set; } = true;

        protected abstract void Tick();
    }
}
