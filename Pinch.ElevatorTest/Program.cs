using Pinch.ElevatorTest.Events;

namespace Pinch.ElevatorTest;

public class Program
{
    static void Main(string[] args)
    {
        TestElevator elevator  = new TestElevator();

        elevator.GroundFloor.PressUp();
        Console.WriteLine(elevator.Car.Next());
        elevator.Car.RequestFloor(elevator.L5);
        Console.WriteLine(elevator.Car.Next()); // close

        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());
        Console.WriteLine(elevator.Car.Next());





    }
}
