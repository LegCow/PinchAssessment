namespace Pinch.ElevatorTest.Events;

public class DoorsOpenEvent : IElevatorEvent
{
    public string Name => "DoorsOpen";

    public Floor CurrentFloor { get; }
    public DoorsOpenEvent(Floor currentFloor)
    {
        CurrentFloor = currentFloor;
    }

    public override string ToString()
    {
        return $"{ Name } on { CurrentFloor.Name }";
    }
}
