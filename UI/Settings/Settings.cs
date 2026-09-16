using Game.Utilities.Autoloads;
using Godot;
using Utils;

namespace Game.UI.Settings;

public partial class Settings : UIScreen
{
    public override void _ShortcutInput(InputEvent @event)
    {
        if (@event.IsActionPressed("escape"))
        {
            UIManager.Instance.Pop();
        }
    }

    public override void OnPushed()
    {
        ConfigManager.Instance.LoadConfig();
    }

    public override void OnPopped()
    {
        ConfigManager.Instance.SaveConfig();
    }
}
