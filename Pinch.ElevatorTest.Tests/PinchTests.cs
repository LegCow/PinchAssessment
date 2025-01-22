using Pinch.ElevatorTest.Events;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Pinch.ElevatorTest.Tests
{
    [TestClass]
    public sealed class PinchTests
    {
        [TestMethod]
        public void TestScenario1()
        {
            // Passenger summons lift on the ground floor. Once in, choose to go to level 5.
            var elevator = new TestElevator();
            
            elevator.GroundFloor.PressUp();

            AssertNextDoorOpensAtFloor(elevator, elevator.GroundFloor); // Ding

            // Passenger enters
            // Passenger pressed L5
            elevator.Car.RequestFloor(elevator.L5);
            AssertNextDoorClosesAtFloor(elevator, elevator.GroundFloor);
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextDoorOpensAtFloor(elevator, elevator.L5); // Ding

            // Passenger exits
        }

        [TestMethod]
        public void TestScenario2()
        {
            // Passenger summons lift on level 6 to go down.
            // Passenger on level 4 summons the lift to go down.
            // They both choose L1.

            var elevator = new TestElevator();

            elevator.L6.PressDown();
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextFloorIs(elevator, elevator.L6);
            AssertNextDoorOpensAtFloor(elevator, elevator.L6);

            // Passenger 1 enters
            elevator.Car.RequestFloor(elevator.GroundFloor);
            AssertNextDoorClosesAtFloor(elevator, elevator.L6);

            // Passenger 2 requests down
            elevator.L4.PressDown();            
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextDoorOpensAtFloor(elevator, elevator.L4);

            // Passenger 2 enters
            elevator.Car.RequestFloor(elevator.GroundFloor); // this is not required as it's already selected
            AssertNextDoorClosesAtFloor(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.GroundFloor);
            AssertNextDoorOpensAtFloor(elevator, elevator.GroundFloor);

            // Both passengers leave
        }


        [TestMethod]
        public void TestScenario3()
        {
            // Passenger 1 summons lift to go up from L2.
            // Passenger 2 summons lift to go down from L4.
            // Passenger 1 chooses to go to L6.
            // Passenger 2 chooses to go to Ground Floor

            var elevator = new TestElevator();

            elevator.L2.PressUp();
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextDoorOpensAtFloor(elevator, elevator.L2);

            elevator.Car.RequestFloor(elevator.L6);

            elevator.L4.PressDown();

            AssertNextDoorClosesAtFloor(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextFloorIs(elevator, elevator.L6);
            AssertNextDoorOpensAtFloor(elevator, elevator.L6);

            // Passenger 1 exits

            AssertNextDoorClosesAtFloor(elevator, elevator.L6);
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextDoorOpensAtFloor(elevator, elevator.L4);

            // Passenger 2 enters
            elevator.Car.RequestFloor(elevator.GroundFloor);
            AssertNextDoorClosesAtFloor(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.GroundFloor);
            AssertNextDoorOpensAtFloor(elevator, elevator.GroundFloor);

            // Passenger 2 exits
        }

        [TestMethod]
        public void TestScenario4()
        {
            // Passenger 1 summons lift to go up from Ground. They choose L5.
            // Passenger 2 summons lift to go down from L4.
            // Passenger 3 summons lift to go down from L10.
            // Passengers 2 and 3 choose to travel to Ground.

            var elevator = new TestElevator();

            elevator.GroundFloor.PressUp();
            AssertNextDoorOpensAtFloor(elevator, elevator.GroundFloor);
            AssertNextDoorClosesAtFloor(elevator, elevator.GroundFloor);
            elevator.Car.RequestFloor(elevator.L5);

            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.L2);

            elevator.L4.PressDown(); // Passenger 2

            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L4);
            AssertNextFloorIs(elevator, elevator.L5);

            elevator.L10.PressDown();

            AssertNextDoorOpensAtFloor(elevator, elevator.L5);
            // Passenger 1 exits
            AssertNextDoorClosesAtFloor(elevator, elevator.L5);

            AssertNextFloorIs(elevator, elevator.L6);
            AssertNextFloorIs(elevator, elevator.L7);
            AssertNextFloorIs(elevator, elevator.L8);
            AssertNextFloorIs(elevator, elevator.L9);
            AssertNextFloorIs(elevator, elevator.L10);

            AssertNextDoorOpensAtFloor(elevator, elevator.L10);
            // Passenger 3 enters
            elevator.Car.RequestFloor(elevator.GroundFloor);
            AssertNextDoorClosesAtFloor(elevator, elevator.L10);

            AssertNextFloorIs(elevator, elevator.L9);
            AssertNextFloorIs(elevator, elevator.L8);
            AssertNextFloorIs(elevator, elevator.L7);
            AssertNextFloorIs(elevator, elevator.L6);
            AssertNextFloorIs(elevator, elevator.L5);
            AssertNextFloorIs(elevator, elevator.L4);

            AssertNextDoorOpensAtFloor(elevator, elevator.L4);
            // Passenger 2 enters
            // elevator.Car.RequestFloor(elevator.GroundFloor); // Not needed so i'll skip this one. Passenger see's ground floor highlighted
            AssertNextDoorClosesAtFloor(elevator, elevator.L4);

            AssertNextFloorIs(elevator, elevator.L3);
            AssertNextFloorIs(elevator, elevator.L2);
            AssertNextFloorIs(elevator, elevator.L1);
            AssertNextFloorIs(elevator, elevator.GroundFloor);

            AssertNextDoorOpensAtFloor(elevator, elevator.GroundFloor);
            // Passengers 2 and 3 exit
        }

        private void AssertNextFloorIs(TestElevator elevator, Floor floor)
        {
            Assert.IsTrue(elevator.Car.Next() is ElevatorArrivedEvent e1 && e1.ArrivedFloor == floor);
        }

        private void AssertNextDoorOpensAtFloor(TestElevator elevator, Floor floor)
        {
            Assert.IsTrue(elevator.Car.Next() is DoorsOpenEvent e1 && e1.CurrentFloor == floor);
        }

        private void AssertNextDoorClosesAtFloor(TestElevator elevator, Floor floor)
        {
            Assert.IsTrue(elevator.Car.Next() is DoorsClosedEvent e1 && e1.CurrentFloor == floor);
        }
    }
}
