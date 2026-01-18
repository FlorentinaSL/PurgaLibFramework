using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Items;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.EventsArgs;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.Handlers
{
    public static class CustomItemHandler
    {
        public static readonly List<CustomItem> Registered = new();

        internal static readonly Dictionary<Item, CustomItem> ItemMap = new();
        
        public static Event<CustomItemDroppedEventArgs> DroppedItem = new();
        public static Event<CustomItemPickedUpEventArgs> PickedUpItem = new();
        public static Event<CustomItemUsedEventArgs> UsedItem = new();
        
        public static void Register(CustomItem customItem)
        {
            if (customItem == null || Registered.Contains(customItem)) return;
            Registered.Add(customItem); 
        }
        
        public static void Give(Player player, CustomItem custom)
        {
            if (player == null || custom == null) return;


            Item item = new Item(custom.BaseType);

            player.Inventory.ServerAddItem(custom.Type, ItemAddReason.Undefined, item.Serial);
            ItemMap[item] = custom;
        }



        internal static bool TryGet(Item item, out CustomItem custom)
        {
            custom = null;
            if (item == null) return false;
            return ItemMap.TryGetValue(item, out custom);
        }

        internal static void RemoveItemFromMap(Item item)
        {
            if (item != null)
            {
                ItemMap.Remove(item);
            }
        }
        
        public static void OnItemUsed(Player player, Item item)
        {
            if (!TryGet(item, out var custom)) return;

            custom.OnUse(player);
            UsedItem?.Invoke(new CustomItemUsedEventArgs(player, item));
        }

        public static void OnItemDropped(Player player, Item item)
        {
            if (!TryGet(item, out var custom)) return;

            custom.OnDrop(player);
            DroppedItem?.Invoke(new CustomItemDroppedEventArgs(player, item));

            RemoveItemFromMap(item);
        }

        public static void OnItemPickedUp(Player player, Item item)
        {
            if (!TryGet(item, out var custom)) return;

            custom.OnPickup(player);
            PickedUpItem?.Invoke(new CustomItemPickedUpEventArgs(player, item));
        }
    }
}
