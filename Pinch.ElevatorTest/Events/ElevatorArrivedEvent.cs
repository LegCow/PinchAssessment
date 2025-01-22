namespace Pinch.ElevatorTest.Events;

public class ElevatorArrivedEvent : IElevatorEvent
{
    public string Name => "ElevatorArrived";
    public Floor ArrivedFloor { get; }

    public ElevatorArrivedEvent(Floor arrivedFloor)
    {
        ArrivedFloor = arrivedFloor;
    }

    public override string ToString()
    {
        return $"{ Name } on { ArrivedFloor.Name }";
    }
}
