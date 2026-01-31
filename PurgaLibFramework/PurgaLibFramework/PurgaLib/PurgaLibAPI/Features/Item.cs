using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public class Item
    {
        public ItemBase Base { get; }
        
        public ItemType Type => Base.ItemTypeId;
        
        public Player Owner { get; }
        
        public ushort Serial => Base.ItemSerial;
        
        public bool IsEquipped => Owner != null && Owner.Inventory.CurItem.SerialNumber == Serial;
        
        public bool IsPickup => Pickup != null;
        
        #pragma warning disable
        public ItemPickupBase Pickup { get; }
        
        public Vector3 Position =>
            Pickup != null ? Pickup.transform.position :
            Owner != null ? Owner.Transform.position :
            Vector3.zero;

        #region Constructors
        

        public Item(ItemType type, Player owner = null)
        {
            if (owner == null) return;

            Base = owner.Inventory.ServerAddItem(type, ItemAddReason.Undefined);
            Owner = owner;
        }
        
        #endregion

        #region Type helpers

        public bool IsWeapon => Base is Firearm;
        public bool IsConsumable => Base is Consumable;
        public bool IsKeycard => Type.ToString().Contains("Keycard");

        public Firearm Firearm => Base as Firearm;
        public Consumable Consumable => Base as Consumable;

        #endregion

        #region Actions
        
        public void Destroy()
        {
            if (Pickup != null)
            {
                Object.Destroy(Pickup.gameObject);
                return;
            }

            Owner?.Inventory.ServerRemoveItem(Serial, null);
        }
        
        public void Drop(Vector3 randomPoint)
        {
            if (Owner == null) return;
            Owner.Inventory.ServerDropItem(Serial);
        }
        
        public void Give(Player target)
        {
            if (Owner == null || target == null) return;

            Owner.Inventory.ServerRemoveItem(Serial, null);
            target.Inventory.ServerAddItem(Type, ItemAddReason.Undefined, Serial);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return $"Item(Type={Type}, Serial={Serial}, Owner={Owner?.Nickname ?? "None"})";
        }

        #endregion
    }
}
