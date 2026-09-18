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

    public void RemapActionTo(InputEvent @event)
    {
        if (@event is InputEventKey inputEventKey)
        {
            foreach (var action in InputMap.GetActions())
            {
                if (InputMap.EventIsAction(inputEventKey, action))
                {
                    var oldEvent = InputMap.ActionGetEvents(Action).ElementAtOrDefault(0);

                    InputMap.ActionEraseEvents(action);
                    InputMap.ActionAddEvent(action, oldEvent);

                    if (oldEvent is InputEventKey oldEventKey)
                    {
                        var binding = new KeyBind()
                        {
                            KeyCode = ConfigManager
                                .Instance.InputEventKeytoKeyBind(oldEventKey)
                                .KeyCode,
                        };

                        ConfigManager.Instance.GameSettings.KeysMap[action] = binding;
                    }

                    break;
                }
            }

            InputMap.ActionEraseEvents(Action);
            InputMap.ActionAddEvent(Action, inputEventKey);

            var keyBinding = new KeyBind()
            {
                KeyCode = ConfigManager.Instance.InputEventKeytoKeyBind(inputEventKey).KeyCode,
            };

            ConfigManager.Instance.GameSettings.KeysMap[Action] = keyBinding;
        }
    }

    public void DisplayCurrentKey()
    {
        var currentKey =
            InputMap.ActionGetEvents(Action).ElementAtOrDefault(0)?.AsText() ?? "No key set";
        Text = currentKey;
    }
}
