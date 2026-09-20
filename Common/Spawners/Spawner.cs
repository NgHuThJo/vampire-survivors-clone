using Game.Entities.Enemies;
using Game.Entities.Player;
using Godot;

namespace Game.Common.Spawners;

public partial class Spawner : Node2D
{
    [Export]
    public Timer Timer { get; set; }

    [Export]
    public Player Player { get; set; }

    [Export]
    public PathFollow2D Path { get; set; }

    [Export]
    public PackedScene Entity { get; set; }
    private const float PATH_SPEED = 0.25f;

    public override void _Ready()
    {
        Timer.Timeout += OnTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        Path.ProgressRatio = (Path.ProgressRatio + PATH_SPEED * (float)delta) % 1;
    }

    public override void _ExitTree()
    {
        Timer.Timeout -= OnTimeout;
    }

    public void OnTimeout()
    {
        var child = Entity.Instantiate<Enemy>();
        child.Initialize(Player);

        AddChild(child);
        child.GlobalPosition = Path.GlobalPosition;
        child.GlobalRotation = Path.GlobalRotation;
    }
}
