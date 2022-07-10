using System.Linq;
using Godot;

public class ObstacleShooter : Sprite
{

    [Export] public PackedScene ObstacleResource;
    [Export] public bool ShootLeft;
    [Export] public float Speed;
    [Export] public int[] Pattern;
    [Export] public int XBound;
		[Export] public bool Rideable = false;

    private int PatternIndex = 0;
    private float Reloading = 0;

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        Reloading -= delta;
        if (Reloading <= 0.0)
        {
            Reloading = Pattern[PatternIndex];
            PatternIndex = (PatternIndex + 1) % Pattern.Count();

            var obstacle = ObstacleResource.Instance() as Obstacle;
            obstacle.Position = Vector2.Zero;
            obstacle.Speed = Speed;
            obstacle.MoveLeft = ShootLeft;
            obstacle.XBound = XBound;
            obstacle.Active = true;
						obstacle.CanRide = Rideable;
            AddChild(obstacle);
        }
    }
}
