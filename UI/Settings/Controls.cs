using Game.Utilities.Autoloads;
using Godot;

namespace Game.UI.Settings;

public partial class Controls : PanelContainer
{
    public override void _Ready() { }

    public void LoadKeyBindings()
    {
        var keysMap = ConfigManager.Instance.GameSettings.KeysMap;

        foreach (var (key, value) in keysMap)
        {
            if (InputMap.HasAction(key)) { }
        }
    }
}
