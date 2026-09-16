using System.Collections.Generic;
using System.Linq;
using Game.Utilities.Autoloads;
using Godot;

namespace Game.UI.Settings;

public partial class RebindButton : Button
{
    [Export]
    public Label CurrentKeyText { get; set; }

    [Export]
    public string Action { get; private set; }

    public override void _Ready()
    {
        SetProcessUnhandledKeyInput(false);
        InitializeKeyBinding();
        DisplayCurrentKey();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Keycode != Key.Escape)
        {
            RemapActionTo(@event);
            ButtonPressed = false;
        }
    }

    public override void _Toggled(bool isButtonPressed)
    {
        SetProcessUnhandledKeyInput(isButtonPressed);

        if (isButtonPressed)
        {
            Text = "<press a key>";
            Modulate = Colors.Yellow;
            ReleaseFocus();
        }
        else
        {
            DisplayCurrentKey();
            Modulate = Colors.White;
            GrabFocus();
        }
    }

    public void InitializeKeyBinding()
    {
        foreach (var key in ConfigManager.Instance.GameSettings.KeysMap)
        {
            GD.Print(key.Key, ", ", key.Value);
        }

        GD.Print(InputMap.ActionGetEvents(Action));

        var inputEventKey = new InputEventKey();

        if (ConfigManager.Instance.GameSettings.KeysMap.TryGetValue(Action, out var value))
        {
            inputEventKey.Keycode = value.KeyCode;
        }
        else
        {
            inputEventKey.Keycode = InputMap
                .ActionGetEvents(Action)
                .Cast<InputEventKey>()
                .ElementAt(0)
                .Keycode;
        }

        Text = inputEventKey.AsTextKeyLabel();
    }

    public void RemapActionTo(InputEvent @event)
    {
        InputMap.ActionEraseEvents(Action);
        InputMap.ActionAddEvent(Action, @event);

        var inputEventKey = (InputEventKey)@event;

        var keyBinding = new KeyBind() { KeyCode = inputEventKey.Keycode };

        ConfigManager.Instance.GameSettings.KeysMap[Action] = keyBinding;
    }

    public void DisplayCurrentKey()
    {
        var currentKey =
            InputMap.ActionGetEvents(Action).ElementAtOrDefault(0)?.AsText() ?? "No key set";
        Text = currentKey;
    }
}
