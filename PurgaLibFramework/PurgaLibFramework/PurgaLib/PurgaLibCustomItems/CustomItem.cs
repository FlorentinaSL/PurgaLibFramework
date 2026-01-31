using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;

public abstract class CustomItem
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract SpawnLocationType SpawnLocation { get; }
    public abstract ItemType BaseType { get; }
    public abstract ItemType Type { get; }

    public virtual void OnUse(Player player) { }
    public virtual void OnDrop(Player player) { }
    public virtual void OnPickup(Player player) { }
}
