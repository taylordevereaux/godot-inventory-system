using Godot;
using System;
using System.Linq;

public class Inventory : Resource
{
    public Guid Id => _id;
    private Guid _id;

    [Signal]
    public delegate void ItemChanged(int[] indexes);

    [Export]
    public Item[] Items { get; set; } = new Item[9];

    public ItemDragData DragData { get; set; }

    public Inventory()
    {}

    public Inventory(Guid id)
    {
        _id = id;
    }

    

    public bool CanAddItem()
    {
        return Items.Any(x => x == null);
    }

    public bool TryAddItem(Item item)
    {
        if (CanAddItem())
        {
            int index = -1;
            for (var i = 0; i < Items.Length; ++i)
            {
                if (Items[i] == null)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
                throw new ArgumentOutOfRangeException("Could not find unused inventory index");
            
            SetItem(index, item);

            return true;
        }
        else
            return false;
    }

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
