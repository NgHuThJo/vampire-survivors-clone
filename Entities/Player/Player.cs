using System.Collections.Generic;
using System.Linq;
using Game.Common.Components.CollisionDetection.Hurtbox;
using Game.Common.Components.Combat.Health;
using Game.Common.Components.Combat.Movement;
using Game.World.Maps;
using Godot;

namespace Game.Entities.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerController Controller { get; private set; }

    [Export]
    public HealthComponent Health { get; private set; }

    [Export]
    public MovementComponent Movement { get; private set; }

    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public Map Map { get; private set; }

    [Export]
    public PlayerData Data { get; private set; }
    public PlayerStateMachine StateMachine { get; init; } = new();

    public Dictionary<Vector2, float> RotationMap { get; init; } =
        new()
        {
            { Vector2.Up, 270 },
            { Vector2.Right, 0 },
            { Vector2.Down, 90 },
            { Vector2.Left, 180 },
        };

    public override void _Ready()
    {
        Initialize();

        StateMachine.ChangeState(new PlayerMovingState(this, StateMachine));
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print(
        //     $"PLAYER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );

        StateMachine.PhysicsUpdate(delta);
    }

    public void Initialize()
    {
        Health.Initialize(Data.HealthData);
        Movement.Initialize(Data.MovementData);
    }
}
