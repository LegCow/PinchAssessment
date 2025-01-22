namespace Pinch.ElevatorTest;

public class Floor
{
    public Floor(string name, Floor? down = null)
    {
        Name = name;
        if (down != null)
        {
            Down = down;
            down.Up = this;
        }
    }

    public string Name { get; }
    public Floor? Up { get; internal set; }
    public Floor? Down { get; internal set; }
    public ElevatorCar? Car { get; internal set; }
    public bool SummonElevatorUp  { get; internal set; }
    public bool SummonElevatorDown { get; internal set; }


    public void PressUp()
    {
        if (Up == null) throw new InvalidOperationException("Already on top floor (Button wouldn't physically exist)");
        SummonElevatorUp = true;
        GetCar().RequestFloorFromFloor(this);
    }

    public void PressDown()
    {
        if (Down == null) throw new InvalidOperationException("Already on bottom floor (Button wouldn't physically exist)");
        SummonElevatorDown = true;
        GetCar().RequestFloorFromFloor(this);
    }

    public IEnumerable<Floor> GetFloorsUp()
    {
        var current = Up;
        while (current != null)
        {
            yield return current;
            current = current.Up;
        }
    }

    public IEnumerable<Floor> GetFloorsDown()
    {
        var current = Down;
        while (current != null)
        {
            yield return current;
            current = current.Down;
        }
    }

    public bool AreFloorRequestsUp()
    {
        if (SummonElevatorUp)
            return true;
        else
            return Up?.AreFloorRequestsUp() ?? false;
    }

    public bool AreFloorRequestsDown()
    {
        if (SummonElevatorUp)
            return true;
        else
            return Down?.AreFloorRequestsDown() ?? false;
    }

    public ElevatorCar GetCar()
    {
        // Check up
        var current = this;
        while (current != null)
        {
            if (current.Car != null) return current.Car;
            current = current.Up;
        }

        // Check down
        current = this;
        while (current != null)
        {
            if (current.Car != null) return current.Car;
            current = current.Down;
        }

        throw new InvalidOperationException("There's no elevator car");
    }

    public ElevatorDirection GetDirection(Floor nextFloor)
    {
        if (nextFloor == this) return ElevatorDirection.None;
        if (nextFloor == Up) return ElevatorDirection.Up;
        if (nextFloor == Down) return ElevatorDirection.Down;

        // Recursively check. None is probably an error state here!
        return Up?.GetDirection(nextFloor) ?? Down?.GetDirection(nextFloor) ?? ElevatorDirection.None;
    }
}
