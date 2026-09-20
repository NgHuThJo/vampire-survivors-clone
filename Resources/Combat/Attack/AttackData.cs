using Godot;

namespace Game.Resources.Combat.Attack;

[GlobalClass]
public partial class AttackData : Resource
{
    [Export]
    public float Damage { get; set; }

    [Export]
    public float Cooldown { get; set; }
}
