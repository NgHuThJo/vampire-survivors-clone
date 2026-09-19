using Game.Entities.Enemies;
using Godot;

namespace Game.Common.Spawners;

public partial class Spawner : Node2D
{
    [Export]
    public Timer Timer { get; set; }

    [Export]
    public PathFollow2D Path { get; set; }

    [Export]
    public PackedScene Entity { get; set; }

    public override void _Ready()
    {
        Timer.Timeout += OnTimeout;
    }

    public override void _ExitTree()
    {
        Timer.Timeout -= OnTimeout;
    }

    public void OnTimeout()
    {
        var child = Entity.Instantiate<Enemy>();

        AddChild(child);
        child.GlobalPosition = Path.GlobalPosition;
        child.GlobalRotation = Path.GlobalRotation;
    }
}
