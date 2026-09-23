using Game.Common.Components;
using Game.Common.Components.CollisionDetection.Hurtbox;
using Game.Common.Components.Combat.Health;
using Game.Common.Components.Combat.Movement;
using Game.Utilities.Autoloads;
using Godot;

namespace Game.Entities.Enemies;

public partial class Enemy : CharacterBody2D, IDamageable
{
    [Export]
    public MovementComponent Movement { get; private set; }

    [Export]
    public HealthComponent Health { get; private set; }

    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public TextureProgressBar HealthBar { get; private set; }

    [Export]
    public EnemyData Data { get; set; }
    public Player.Player Player { get; private set; }

    private bool HasDied { get; set; } = false;

    public override void _Ready()
    {
        Movement.Initialize(Data.MovementData);

        Health.NoHealthLeft += OnNoHealthLeft;
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
        Movement.ApplyVelocity(direction);
        MoveAndSlide();
    }

    public override void _ExitTree()
    {
        Health.NoHealthLeft -= OnNoHealthLeft;
    }

    public void Initialize(Player.Player player)
    {
        Player = player;
        HealthBar.MaxValue = Data.HealthData.MaxHealth;
        HealthBar.Value = Data.HealthData.MaxHealth;

        GD.Print("Max health: ", HealthBar.MaxValue, ", Current health: ", HealthBar.Value);
    }

    public async void OnNoHealthLeft(NoHealthLeft noHealthLeft)
    {
        if (HasDied || noHealthLeft.Source is not Enemy)
        {
            return;
        }

        HasDied = true;

        // EffectManager.Instance.Spawn<Explosion>(LoadedVfx.Explosion, GlobalPosition);
        // AudioManager.Instance.PlaySfx(LoadedSfx.Explosion);

        QueueFree();
        // var context = new EnemyDied { Points = Data.Points };
        // EventBus.Instance.EnemyDied.Invoke(context);
    }

    public void ReceiveDamage(IDamageContext context)
    {
        var damage = DamageManager.Instance.ApplyDamage(context);

        Health.TakeDamage(damage);
    }
}
