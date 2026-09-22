using Game.Common.Components;
using Game.Common.Components.CollisionDetection.Hitbox;
using Game.Resources.Weapons;
using Godot;

namespace Game.Entities.Weapons;

public partial class Garlic : Node2D
{
    [Export]
    public AoeWeaponData WeaponData { get; private set; }

    [Export]
    public Timer CooldownTimer { get; private set; }

    [Export]
    public HitboxComponent Hitbox { get; private set; }

    public override void _Ready()
    {
        CooldownTimer.WaitTime = WeaponData.Cooldown;
        var shape = Hitbox.CollisionShape.Shape;

        if (shape is CircleShape2D circle)
        {
            GD.Print("Shape of garlic is circle");
            circle.Radius = WeaponData.Radius;
        }

        CooldownTimer.Timeout += OnTimeout;
    }

    public override void _ExitTree()
    {
        CooldownTimer.Timeout -= OnTimeout;
    }

    public void OnTimeout()
    {
        foreach (var body in Hitbox.GetOverlappingBodies())
        {
            if (body is IDamageable damageable)
            {
                var context = new AoeWeaponContext()
                {
                    Source = this,
                    Target = body,
                    Data = WeaponData,
                };

                damageable.ReceiveDamage(context);
            }
        }
    }
}
