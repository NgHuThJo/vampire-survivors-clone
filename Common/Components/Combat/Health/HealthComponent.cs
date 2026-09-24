using System;
using Game.Common.Components.CollisionDetection.Hurtbox;
using Game.Resources.Combat.Health;
using Godot;

namespace Game.Common.Components.Combat.Health;

public partial class HealthComponent : Node
{
    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    public event Action<HealthChanged> HealthChanged;
    public event Action<NoHealthLeft> NoHealthLeft;
    public HealthData Data { get; private set; }
    public int CurrentHealth { get; private set; }

    public void Initialize(HealthData data)
    {
        Data = data;
        CurrentHealth = data.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Data.MaxHealth);

        var healthChanged = new HealthChanged() { CurrentHealth = CurrentHealth };

        HealthChanged?.Invoke(healthChanged);

        if (IsDead)
        {
            var noHealthLeft = new NoHealthLeft();

            NoHealthLeft?.Invoke(noHealthLeft);
        }
    }

    public bool IsDead => CurrentHealth <= 0;
}
