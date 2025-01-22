namespace Pinch.ElevatorTest;

public class ElevatorDoorState
{
    public static readonly ElevatorDoorState ClosedState = new ElevatorDoorState("CLOSED");
    //public static readonly ElevatorDoorState OpeningState = new ElevatorDoorState("OPENING");
    public static readonly ElevatorDoorState OpenState = new ElevatorDoorState("OPEN");
    //public static readonly ElevatorDoorState ClosingState = new ElevatorDoorState("CLOSING");
    
    
    private ElevatorDoorState(string name)
    {
        Name = name;
    }

    public string Name { get; }

}
