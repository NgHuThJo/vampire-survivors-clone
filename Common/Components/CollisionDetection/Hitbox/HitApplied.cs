using Game.Common.GameEvents;
using Game.Resources.Combat.Attack;

namespace Game.Common.Components.CollisionDetection.Hitbox;

public record HitApplied : IGameEvent
{
    public AttackData Attack { get; init; }
}
