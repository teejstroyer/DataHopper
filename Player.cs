using Godot;

public class Player : Node2D
{

    [Export] public int TileSize = 64;

    [Export] public float MovementSpeed = 10;

    public Tween Tween;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Tween = GetNode<Tween>("Tween");
    }

    public void Move(Vector2 direction)
    {
        Tween.InterpolateProperty(this, nameof(Position).ToLower(), Position, Position + direction * TileSize, 1 / MovementSpeed);
        Tween.Start();
    }
}

