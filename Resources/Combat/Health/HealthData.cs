using Godot;

namespace Game.Resources.Combat.Health;

[GlobalClass]
public partial class HealthData : Resource
{
    [Export]
    public int MaxHealth { get; set; }
}
