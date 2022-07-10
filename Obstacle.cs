using Godot;

public class Obstacle : Area2D
{
    private bool _Active;
    [Export]
    public bool Active
    {
        get => _Active; 
        set {
            _Active = value;
            SetProcess(value);
        }
    }

    [Export] public bool MoveLeft = true;

    [Export] public int XBound = 0;

    [Export] public float Speed = 50f;

		[Export] public bool CanRide;


    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        GlobalPosition += new Vector2(MoveLeft?-1:1 * Speed * delta, 0);
        
        if(MoveLeft && GlobalPosition.x < XBound) Active = false; 
        else if(!MoveLeft && GlobalPosition.x > XBound) Active = false;
    }
}
