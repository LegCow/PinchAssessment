namespace Pinch.ElevatorTest.Events;

public class DoorsClosedEvent : IElevatorEvent
{
    public string Name => "DoorsClosed";

    public Floor CurrentFloor { get; }
    public DoorsClosedEvent(Floor currentFloor)
    {
        CurrentFloor = currentFloor;
    }

    public override string ToString()
    {
        return $"{ Name } on { CurrentFloor.Name }";
    }
}
