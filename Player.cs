using Godot;

public class Player : Area2D
{

    [Export] public int TileSize = 64;

    [Export] public float MovementSpeed = 10;

    public Tween Tween;

    private Obstacle Ride;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Tween = GetNode<Tween>("Tween");
        Connect("area_entered", this, nameof(OnObstacleHit));
        Connect("area_exited", this, nameof(OnObstacleExit));
    }

    public void MoveToGridSpot(int gridx, int gridy) => GlobalPosition = NormalizedPosition(gridx, gridy);

    public Vector2 NormalizedPosition(float x, float y) => new Vector2(x, y) * TileSize - Vector2.One * (TileSize / 2);

    public Vector2 RealGridPosition() => Position + Vector2.One * (TileSize / 2);

    public void Move(Vector2 direction)
    {
        //NEED TO NORMALIZE THE FROG ON THE GRID, ESPECIALLY AFTER LEAVING A LOG
        //Adjust frogs x position to be on grid before jump starts
        var pos = RealGridPosition();
        //GlobalPosition = new Vector2(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.y)) * TileSize - Vector2.One * (TileSize/2);
        MoveToGridSpot(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));

        Tween.InterpolateProperty(this, nameof(Position).ToLower(), Position, Position + direction * TileSize, 1 / MovementSpeed);
        Tween.Start();
    }

    public void OnObstacleExit(Area2D area2D)
    {
        Ride = null;
    }

    public void OnObstacleHit(Area2D area2D)
    {
        var obstacle = area2D as Obstacle;
        GD.Print($" {obstacle.Name}: {obstacle.CanRide}");
        if (obstacle.CanRide)
        {
            Ride = obstacle;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (Ride != null)
        {
            GlobalPosition += new Vector2(Ride.MoveLeft ? -1 : 1 * Ride.Speed * delta, 0);
        }
    }
}

