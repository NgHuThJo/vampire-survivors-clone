using Game.Resources.Weapons;
using Godot;

namespace Game.Entities.Weapons;

public partial class Garlic : Node2D
{
    [Export]
    public AoeWeaponData WeaponData { get; private set; }

    [Export]
    public Timer CooldownTimer { get; private set; }
}
