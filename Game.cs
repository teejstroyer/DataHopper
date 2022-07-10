using Godot;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Game : Node2D
{
    [Export] public int TileSize = 64;
    [Export] public CollisionShape2D Collider;

    private readonly Dictionary<string, Vector2> Directions = new Dictionary<string, Vector2>()
    {
      {"ui_up",Vector2.Up},
      {"ui_down",Vector2.Down},
      {"ui_left",Vector2.Left},
      {"ui_right",Vector2.Right}
    };

    private Player Player1;
    private readonly int Columns = 12;
    private readonly int Rows = 15;
    private Dictionary<int, ObstacleShooter> ObstacleShooters = new Dictionary<int, ObstacleShooter>();

    public override void _Ready()
    {
        Player1 = GetNode<Player>("Player");
				Player1.MoveToGridSpot(Columns/2, Rows);

        var shooters = GetNode<Node2D>("Shooters")?.GetChildren();

        foreach (ObstacleShooter shooter in shooters) 
        {
            var stripped = int.TryParse(Regex.Replace(shooter.Name, "[^0-9]", ""), out int id );
            if (stripped) {
                ObstacleShooters.Add(id, shooter);
            } 
        }

    }


    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (Player1.Tween.IsActive()) return;
        foreach (var key in Directions.Keys)
        {
            if (@event.IsActionPressed(key))
            {
                var dir = Directions[key];
                if (ValidateMove(dir))
                {
                    Player1.Move(Directions[key]);
                }
                break;
            }
        }
    }

    private bool ValidateMove(Vector2 direction) 
    {
        var future = Player1.Position/TileSize + direction;
        return !(future.x < 0 || future.x >= Columns || future.y < 0 || future.y >= Rows);
    }


    public override void _Process(float delta)
    {
    }

}
