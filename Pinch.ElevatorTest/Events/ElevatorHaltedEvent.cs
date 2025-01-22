namespace Pinch.ElevatorTest.Events;

public class ElevatorHaltedEvent : IElevatorEvent
{
    public string Name => "ElevatorHalted";
    public Floor HaltedFloor { get; }

    public ElevatorHaltedEvent(Floor haltedFloor)
    {
        HaltedFloor = haltedFloor;
    }

    public override string ToString()
    {
        return $"{ Name } on { HaltedFloor.Name }";
    }
}
