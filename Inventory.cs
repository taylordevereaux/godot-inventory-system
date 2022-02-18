using Godot;
using System;

public class Inventory : Resource
{
    [Signal]
    public delegate void ItemChanged(int[] indexes);

    [Export]
    public Item[] Items { get; set; } = new Item[9];

    public ItemDragData DragData { get; set; }

    public Item SetItem(int itemIndex, Item item)
    {
        var existingItem = Items[itemIndex];
        Items[itemIndex] = item;
        EmitSignal(nameof(ItemChanged), new int[] {itemIndex});
        return existingItem;
    }

    public void SwapItems(int itemIndex, int targetItemIndex)
    {
        var item = Items[itemIndex];
        Items[itemIndex] = Items[targetItemIndex];
        Items[targetItemIndex] = item;
        EmitSignal(nameof(ItemChanged), new int[] { itemIndex, targetItemIndex });
    }

    public Item RemoveItem(int itemIndex)
    {
        var item = Items[itemIndex];
        Items[itemIndex] = null;
        EmitSignal(nameof(ItemChanged), new int[] { itemIndex });
        return item;
    }

}
