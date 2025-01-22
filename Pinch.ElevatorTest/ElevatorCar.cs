using Pinch.ElevatorTest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinch.ElevatorTest;

public class ElevatorCar
{

    public ElevatorCar(Floor currentFloor)
    {
        CurrentFloor = currentFloor;
        CurrentFloor.Car = this; // Bit of a bidirectional relationship which can be awkward if they disagree, better ways to do this.

        Direction = ElevatorDirection.None;
        Doors = new ElevatorDoors();
        CarRequestSelections = new List<Floor>();
        FloorRequestSelections = new List<Floor>();
    }

    public Floor CurrentFloor { get; private set; }
    public ElevatorDoors Doors { get; private set; }
    public ElevatorDirection Direction { get; private set; }
    public List<Floor> CarRequestSelections { get; private set; }
    public List<Floor> FloorRequestSelections { get; private set; }


    // The logical handler here could be moved into a dedicated logic class, backed by interface to handle multiple behaviors.
    public IElevatorEvent? Next()
    {
        if (Doors.AreOpen)
        {
            Doors.Close();
            return new DoorsClosedEvent(CurrentFloor);
        }

        if (Doors.AreClosed && Direction == ElevatorDirection.Up)
        {
            if (CarRequestSelections.Contains(CurrentFloor) || CurrentFloor.SummonElevatorUp)
            {
                Doors.Open();
                CarRequestSelections.Remove(CurrentFloor);
                CurrentFloor.SummonElevatorUp = false;
                return new DoorsOpenEvent(CurrentFloor);
            }



            // go further up?
            if (CurrentFloor.Up != null && CurrentFloor.GetFloorsUp().Any(floor => floor.SummonElevatorUp || CarRequestSelections.Contains(floor) || FloorRequestSelections.Contains(floor)))
            {
                CurrentFloor = CurrentFloor.Up;
                return new ElevatorArrivedEvent(CurrentFloor);
            }

            if (FloorRequestSelections.Contains(CurrentFloor))
            {
                Doors.Open();
                FloorRequestSelections.Remove(CurrentFloor);
                CurrentFloor.SummonElevatorUp = false;
                return new DoorsOpenEvent(CurrentFloor);
            }

            // Change direction
            if (CurrentFloor.Down != null && CurrentFloor.GetFloorsDown().Any(floor => floor.SummonElevatorDown || CarRequestSelections.Contains(floor)))
            {
                Direction = ElevatorDirection.Down;
                CurrentFloor = CurrentFloor.Down;
                return new ElevatorArrivedEvent(CurrentFloor);
            }

            Direction = ElevatorDirection.None; // Halt
            return null;
        }


        if (Doors.AreClosed && Direction == ElevatorDirection.Down)
        {

            if (CarRequestSelections.Contains(CurrentFloor) || CurrentFloor.SummonElevatorDown)
            {
                Doors.Open();
                CarRequestSelections.Remove(CurrentFloor);
                CurrentFloor.SummonElevatorDown = false;
                return new DoorsOpenEvent(CurrentFloor);
            }



            // go further down?
            if (CurrentFloor.Down != null && CurrentFloor.GetFloorsDown().Any(floor => floor.SummonElevatorDown || CarRequestSelections.Contains(floor) || FloorRequestSelections.Contains(floor)))
            {
                CurrentFloor = CurrentFloor.Down;
                return new ElevatorArrivedEvent(CurrentFloor);
            }

            if (FloorRequestSelections.Contains(CurrentFloor))
            {
                Doors.Open();
                FloorRequestSelections.Remove(CurrentFloor);
                CurrentFloor.SummonElevatorDown = false;
                return new DoorsOpenEvent(CurrentFloor);
            }

            // change direction?
            if (CurrentFloor.Up != null && CurrentFloor.GetFloorsUp().Any(floor => floor.SummonElevatorUp || CarRequestSelections.Contains(floor)))
            {
                Direction = ElevatorDirection.Up;
                CurrentFloor = CurrentFloor.Up;

            }

            Direction = ElevatorDirection.None; // Halt
            return null;
        }

        if (CurrentFloor.SummonElevatorUp)
        {
            Doors.Open();
            CurrentFloor.SummonElevatorUp = false;
            CarRequestSelections.Remove(CurrentFloor);
            return new DoorsOpenEvent(CurrentFloor);
        }

        if (CurrentFloor.SummonElevatorDown)
        {
            Doors.Open();
            CurrentFloor.SummonElevatorDown = false;
            CarRequestSelections.Remove(CurrentFloor);
            return new DoorsOpenEvent(CurrentFloor);
        }

        return null;
    }

    public void RequestFloor(Floor floor)
    {
        if (!CarRequestSelections.Contains(floor))
            CarRequestSelections.Add(floor);

        // The first press of the floor button would determine the direction. Best test this given no direction as it's possible to press the current floor, then another.
        if (Direction == ElevatorDirection.None)
        {
            Direction = CurrentFloor.GetDirection(floor);
        };
    }

    public void RequestFloorFromFloor(Floor floor)
    {
        if (!FloorRequestSelections.Contains(floor))
            FloorRequestSelections.Add(floor);

        // The first press of the floor button would determine the direction. Best test this given no direction as it's possible to press the current floor, then another.
        if (Direction == ElevatorDirection.None)
        {
            Direction = CurrentFloor.GetDirection(floor);
        };
    }


}
