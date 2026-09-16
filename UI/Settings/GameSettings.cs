using System.Collections.Generic;

namespace Game.UI.Settings;

public record GameSettings
{
    public required int Version { get; init; }
    public float MasterVolume { get; set; } = 1f;

    public float MusicVolume { get; set; } = 1f;

    public float SfxVolume { get; set; } = 1f;
    public Dictionary<string, KeyBind> KeysMap { get; init; } = [];
}
