using Godot;
using System;
public class Item : Resource
{
    [Export]
    public string Name { get; set; } = "";

    [Export]
    public Texture Texture { get; set; }
}
