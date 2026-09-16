using Game.Resources.Combat.Health;
using Game.Resources.Combat.Movement;
using Godot;

namespace Game.Entities.Player;

[GlobalClass]
public partial class PlayerData : Resource
{
    [Export]
    public HealthData HealthData { get; set; }

    [Export]
    public MovementData MovementData { get; set; }
}
