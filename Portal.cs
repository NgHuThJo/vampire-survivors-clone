using Godot;

namespace Game;

public partial class Portal : Node2D
{
    [Export]
    public Area2D Area { get; private set; }

    [Export]
    public Sprite2D Sprite { get; private set; }
    private ShaderMaterial _shaderMaterial;
    private Tween _tween;

    public override void _Ready()
    {
        Area.BodyEntered += OnBodyEntered;
        Area.BodyExited += OnBodyExited;

        _shaderMaterial = (ShaderMaterial)Sprite.Material;
        // _shaderMaterial.SetShaderParameter("radius", 0.15f);
        // _shaderMaterial.SetShaderParameter("breathingStrength", 0.0f);
        // _shaderMaterial.SetShaderParameter("displacementStrength", 0.15f);
    }

    public override void _ExitTree()
    {
        Area.BodyEntered -= OnBodyEntered;
        Area.BodyExited -= OnBodyExited;
    }

    public void AnimatePortal(float radius, float breathingStrength, float displacementStrength)
    {
        _tween?.Kill();

        var tween = CreateTween().SetParallel();
        tween.SetTrans(Tween.TransitionType.Cubic);
        tween.TweenProperty(_shaderMaterial, "shader_parameter/radius", radius, 0.5f);
        tween.TweenProperty(
            _shaderMaterial,
            "shader_parameter/breathingStrength",
            breathingStrength,
            0.5f
        );
        tween.TweenProperty(
            _shaderMaterial,
            "shader_parameter/displacementStrength",
            displacementStrength,
            0.5f
        );
    }

    public void OnBodyEntered(Node2D node)
    {
        AnimatePortal(0.4f, 0.03f, 0.05f);
    }

    public void OnBodyExited(Node2D node)
    {
        AnimatePortal(0.15f, 0.0f, 0.15f);
    }
}
