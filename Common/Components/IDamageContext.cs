using Godot;

namespace Game.Common.Components;

public interface IDamageContext
{
    public Node2D Source { get; init; }
    public Node2D Target { get; init; }
}
