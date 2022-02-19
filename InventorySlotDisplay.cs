using Godot;
using System;

public class InventorySlotDisplay : CenterContainer
{
    private TextureRect _itemTextureRect;
    private InventoryDisplay _inventoryDisplay;
    private InventoryManager _inventoryManager;

    private Guid InventoryId => _inventoryDisplay.Inventory.Id;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _itemTextureRect = GetNode<TextureRect>("ItemTextureRect");    
        _inventoryDisplay = GetParent<InventoryDisplay>();
        _inventoryManager = GD.Load<InventoryManager>("res://InventoryManager.tres");
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
        var item = _inventoryManager.GetItem(InventoryId, index);
        
        var data = new ItemDragData() {
            Index = index,
            Item = item,
            SourceInventoryId = InventoryId
        };

        if (item != null) 
        {
            var dragPreview = new TextureRect();
            dragPreview.Texture = item.Texture;
            SetDragPreview(dragPreview);

            DisplayItem(null);
        }

        _inventoryManager.DragData = data;

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
            _inventoryManager.SwapItems(itemDragData.SourceInventoryId, itemDragData.Index, InventoryId, myIndex);
        }
        _inventoryManager.DragData = null;
    }
}
public class ItemDragData : Godot.Object
{
    public int Index { get; set; }
    public Item Item { get; set; }
    public Guid SourceInventoryId { get; set; }
}
