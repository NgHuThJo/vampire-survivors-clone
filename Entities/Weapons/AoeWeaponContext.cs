using Game.Common.Components;
using Game.Resources.Weapons;
using Godot;

namespace Game.Entities.Weapons;

public record AoeWeaponContext : IDamageContext
{
    public required Node2D Source { get; init; }
    public required Node2D Target { get; init; }
    public required AoeWeaponData Data { get; init; }
}
