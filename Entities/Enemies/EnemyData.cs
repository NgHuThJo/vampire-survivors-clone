using Game.Resources.Combat.Health;
using Game.Resources.Combat.Movement;
using Godot;

namespace Game.Entities.Enemies;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export]
    public MovementData MovementData { get; private set; }

    [Export]
    public HealthData HealthData { get; private set; }
}
