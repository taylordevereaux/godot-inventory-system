using Godot;
using System;

public class InventoryDisplay : GridContainer
{   
    public Inventory Inventory => _inventory;
    private Inventory _inventory;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.Connect(nameof(Inventory.ItemChanged), this, nameof(OnInventoryItemChanged));
        UpdateInventoryDisplay();
    }

    public void OnInventoryItemChanged(int[] indexes)
    {
        foreach (int index in indexes)
        {
            UpdateInventorySlotDisplay(index);
        }
    }
    public void UpdateInventoryDisplay()
    {
        for (int index = 0; index < _inventory.Items.Length; ++index)
        {
            UpdateInventorySlotDisplay(index);
        }
    }
    public void UpdateInventorySlotDisplay(int index)
    {
        var inventorySlotDisplay = GetChild<InventorySlotDisplay>(index);
        var item = _inventory.Items[index];
        inventorySlotDisplay.DisplayItem(item);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsActionReleased("ui_left_mouse"))
        {
            if (_inventory.DragData != null)
            {
                _inventory.SetItem(_inventory.DragData.Index, _inventory.DragData.Item);
            }
        }
    }

}
