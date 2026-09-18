using Game.Common.Cutscenes;
using Game.Common.GameEvents.Global;
using Game.Common.Persistence;
using Game.Entities.Enemies;
using Game.Entities.Player;
using Game.UI;
using Game.UI.HUD;
using Game.Utilities.Autoloads;
using Game.Utilities.Loaded;
using Game.World.Maps;
using Godot;
using Utils;

namespace Game.Utilities.World.Maps;

public partial class Level : Node, ISaveable
{
    [Export]
    public Player Player { get; set; }

    [Export]
    public HUDManager HUD { get; set; }

    [Export]
    public AnimationPlayer AnimationPlayer { get; set; }

    public int CurrentLevel { get; private set; } = 1;

    public override void _Ready()
    {
        HUD.ShowHUD();
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print(
        //     $"LEVEL MANAGER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );
    }

    public override void _ExitTree()
    {
        EventBus.Instance.EnemyDied -= OnEnemyDied;
    }

    public void OnEnemyDied(EnemyDied context) { }

    public void ShowGameoverScreen()
    {
        SaveManager.Instance.Save();

        var instance = LoadedScenes.GameoverScreen.Instantiate<UIScreen>();
        UIManager.Instance.Push(instance);
        HUD.HideHUD();
    }

    public void Save()
    {
        SaveManager.Instance.GameSaveState.HighestLevel = Mathf.Max(
            SaveManager.Instance.GameSaveState.HighestLevel,
            CurrentLevel
        );
    }
}
