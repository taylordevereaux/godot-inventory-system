using Godot;
using System;

public class InventorySlotDisplay : CenterContainer
{
    private TextureRect _itemTextureRect;
    private Inventory _inventory;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _itemTextureRect = GetNode<TextureRect>("ItemTextureRect");    
        _inventory = GD.Load<Inventory>("res://Inventory.tres");  
    }

    public void DisplayItem(Item item)
    {
        if (item != null)
        {
            _itemTextureRect.Texture = item.Texture;
        }
        else
        {
            _itemTextureRect.Texture = GD.Load<Texture>("res://Items/EmptyInventorySlot.png");
        }
    }


    public override object GetDragData(Vector2 position)
    {
        int index = GetIndex();
        var item = _inventory.RemoveItem(index);
        
        // var data = new Godot.Object();
        // data.Set("Item", item);
        // data.Set("Index", index);
        var data = new ItemDragData() {
            Index = index,
            Item = item
        };

        if (item != null) 
        {
            var dragPreview = new TextureRect();
            dragPreview.Texture = item.Texture;
            SetDragPreview(dragPreview);
        }

        _inventory.DragData = data;

        return data;
    }

    public override bool CanDropData(Vector2 position, object data)
    {
        return data is ItemDragData item && item.Item != null;
    }

    public override void DropData(Vector2 position, object data)
    {
        if (data is ItemDragData itemDragData)
        {
            var myIndex = GetIndex();
            var myItem = _inventory.Items[myIndex];
            // if (myItem != null && myItem.Name == itemDragData.Item.Name)
            _inventory.SwapItems(myIndex, itemDragData.Index);
            _inventory.SetItem(myIndex, itemDragData.Item);
        }
        
        _inventory.DragData = null;
    }
}
public class ItemDragData : Godot.Object
{
    public int Index { get; set; }
    public Item Item { get; set; }
}
