using Godot;

namespace Game.UI.Settings;

public record KeyBind
{
    public Key KeyCode { get; init; }
}
