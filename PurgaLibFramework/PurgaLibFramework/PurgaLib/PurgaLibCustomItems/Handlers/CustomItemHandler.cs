using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InventorySystem;
using InventorySystem.Items;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.SpawnLocationMapper;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.EventsArgs;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.EventArgs.Round;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handlers;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.Handlers
{
    public static class CustomItemHandler
    {
        public static readonly List<CustomItem> Registered = new();

        internal static readonly Dictionary<Item, CustomItem> ItemMap = new();

        public static event Action<CustomItemDroppedEventArgs> DroppedItem;
        public static event Action<CustomItemPickedUpEventArgs> PickedUpItem;
        public static event Action<CustomItemUsedEventArgs> UsedItem;

        public static void Register(CustomItem customItem)
        {
            if (customItem == null || Registered.Contains(customItem)) return;
            Registered.Add(customItem);
        }

        public static void RegisterNaturalEvent()
        {
            RoundHandlers.Started += NaturalSpawn;
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

        public static void NaturalSpawn(RoundStartedEventArgs ev)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(x => typeof(CustomItem).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);

            foreach (var type in types)
            {
                try
                {
                    CustomItem instance = (CustomItem)Activator.CreateInstance(type);
                    
                    if (instance.SpawnLocation == SpawnLocationType.None)
                        continue;
                    
                    var spawnPos = LocationMapper.GetPoint(instance.SpawnLocation);

                    if (spawnPos == Vector3.zero)
                    {
                        spawnPos = Room.Random()?.GetRandomPoint() ?? Vector3.zero;
                    }

                    Item item = new Item(instance.BaseType);
                    item.Drop(spawnPos);
                }
                catch (Exception ex)
                {
                    Log.Error($"[Natural Spawn CI] Error spawning {type.Name}: {ex.Message}");
                }
            }
        }
    }
}
