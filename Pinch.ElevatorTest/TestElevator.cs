namespace Pinch.ElevatorTest;

public class TestElevator
{
    public Floor GroundFloor { get; }
    public Floor L1 { get; }
    public Floor L2 { get; }
    public Floor L3 { get; }
    public Floor L4 { get; }
    public Floor L5 { get; }
    public Floor L6 { get; }
    public Floor L7 { get; }
    public Floor L8 { get; }
    public Floor L9 { get; }
    public Floor L10 { get; }

    public ElevatorCar Car { get; }

    public TestElevator()
    {
        GroundFloor = new Floor("Ground Floor");
        L1 = new Floor("L1", GroundFloor);
        L2 = new Floor("L2", L1);
        L3 = new Floor("L3", L2);
        L4 = new Floor("L4", L3);
        L5 = new Floor("L5", L4);
        L6 = new Floor("L6", L5);
        L7 = new Floor("L7", L6);
        L8 = new Floor("L8", L7);
        L9 = new Floor("L9", L8);
        L10 = new Floor("L10", L9);

        Car = new ElevatorCar(GroundFloor);
    }

}
