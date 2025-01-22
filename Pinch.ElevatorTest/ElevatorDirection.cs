namespace Pinch.ElevatorTest;

public class ElevatorDirection
{
    public static readonly ElevatorDirection None = new ElevatorDirection("NONE");
    public static readonly ElevatorDirection Up = new ElevatorDirection("UP");
    public static readonly ElevatorDirection Down = new ElevatorDirection("DOWN");


    private ElevatorDirection(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
