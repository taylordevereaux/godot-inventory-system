using Godot;
using System;

public class MultiInventoryDisplay : GridContainer
{
    [Export]
    public PackedScene InventoryContainer { get; set; }
    private InventoryManager _inventoryManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Randomize();

        // Creating multiple
        _inventoryManager = GD.Load<InventoryManager>("res://InventoryManager.tres");

        for (var i = 0; i < 9; ++i)
        {
            var inv1 = _inventoryManager.CreateInventory();

            inv1.TryAddItem(GetRandomItem());
            inv1.TryAddItem(GetRandomItem());
            inv1.TryAddItem(GetRandomItem());

            var container = InventoryContainer.Instance();
            this.AddChild(container);
            
            var inventoryDisplay = container.GetChild<InventoryDisplay>(0);
            inventoryDisplay.SetInventory(inv1);
            
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private Item GetRandomItem()
    {
        uint rand = GD.Randi() % 3;

        switch (rand)
        {
            case 0:
                return GD.Load<Item>("res://Items/Potion.tres");
            case 1:
                return GD.Load<Item>("res://Items/Ring.tres");
            case 2:
                return GD.Load<Item>("res://Items/Shield.tres");
            case 3:
                return GD.Load<Item>("res://Items/Sword.tres");
            default:
                return GD.Load<Item>("res://Items/Sword.tres");
        }
    }
}
