using Godot;

namespace Game.Resources.Weapons;

[GlobalClass]
public partial class AoeWeaponData : Resource
{
    [Export]
    public float Damage { get; private set; }

    [Export]
    public float Radius { get; private set; }

    [Export]
    public float Cooldown { get; private set; }

    [Export]
    public int MaxLevel { get; private set; }
}
