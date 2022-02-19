using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : Resource
{
    private  Dictionary<Guid, Inventory> _inventories = new Dictionary<Guid, Inventory>();
    public List<Inventory> Inventories => _inventories.Select(x => x.Value).ToList();
    public ItemDragData DragData { get; set; }

    public Inventory CreateInventory() 
    {
        var id = Guid.NewGuid();
        var inventory = new Inventory(id);
        _inventories.Add(id, inventory);
        return inventory;
    }

    public Item GetItem(Guid inventoryId, int itemIndex)
    {
        var inventory = _inventories[inventoryId];
        return inventory.Items[itemIndex];
    }

    public Item SetItem(Guid inventoryId, int itemIndex, Item item)
    {
        var inventory = _inventories[inventoryId];
        return inventory.SetItem(itemIndex, item);
    }

    public void SwapItems(Guid sourceInventoryId, int sourceItemIndex, Guid targetInventoryId, int targetItemIndex)
    {
        var sourceInventory = _inventories[sourceInventoryId];
        var targetInventory = _inventories[targetInventoryId];
        if (sourceInventoryId == targetInventoryId)
        {
            sourceInventory.SwapItems(sourceItemIndex, targetItemIndex);
        }
        else
        {
            var sourceItem = sourceInventory.Items[sourceItemIndex];
            var targetItem = targetInventory.Items[targetItemIndex];
            sourceInventory.SetItem(sourceItemIndex, targetItem);
            targetInventory.SetItem(targetItemIndex, sourceItem);
        }
    }

    public Item RemoveItem(Guid inventoryId, int itemIndex)
    {
        var inventory = _inventories[inventoryId];
        return inventory.RemoveItem(itemIndex);
    }
}
