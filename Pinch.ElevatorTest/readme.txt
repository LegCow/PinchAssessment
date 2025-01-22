### Pinch ElevatorTest



Thanks again for the opportunity to apply, and secondly, thank you very much for this style of code test! I would rather not reinvent some obscure sorting algorythm in O(log something)
I will try and use this file as a way of tracking my thoughts as I put this together. More than once I've been inside and elevator and wondered how the logic in them works, similar to traffic lights.



## The Challenge:
You are in charge of writing software for an elevator (lift) company.
Your task is to write a program to control the travel of a lift for a 10 storey building.
A passenger can summon the lift to go up or down from any floor, once in the lift they can choose the floor they'd like to travel to.
Your program needs to plan the optimal set of instructions for the lift to travel, stop, and open its doors.

Some test cases:
Passenger summons lift on the ground floor. Once in, choose to go to level 5.
Passenger summons lift on level 6 to go down. Passenger on level 4 summons the lift to go down. They both choose L1.
Passenger 1 summons lift to go up from L2. Passenger 2 summons lift to go down from L4. Passenger 1 chooses to go to L6. Passenger 2 chooses to go to Ground Floor
Passenger 1 summons lift to go up from Ground. They choose L5. Passenger 2 summons lift to go down from L4. Passenger 3 summons lift to go down from L10. Passengers 2 and 3 choose to travel to Ground.
	


## Scope
	The test cases listed above give a good idea of the scope of the problem space, I'll try and focus only on those as a part of the solution unless I have plenty of time, become obsessed or maybe continue over the weekend because I'm a nerd :)
	
	


## Concepts and Functions

# State Driven
	This problem looks very state driven. Since elevators operate in realtime, most of the behavior would be defined as state transition and event handling.

# Continuing the direction of travel
	Once a floor is in motion for a given direction it will continue until there are no more floor requests beyond that point.
	For example, if I'm heading from ground floor to level 3, the elevator will continue up to service a user on level 7 rather than go down to level 2.

# Multiple Elevators
	The logic complexity in this case would be much higher. Selecting a floor would run logic to determin which elevator fields this request.
	Outside the scope of this assignment, but would be interesting to model.

# Door State
	

# Between Floor Sensors
	When travelling, there must be a point in which an elevator refuses to stop at a floor. Otherwise a strange timing fault may occur if a user requests the elevator just as it's passing that floor. Instantaneous stop? Hope not!
	So there's probably a trigger/switch on the tracks between floors that apply logic to ignore some requests
	
# Displays, buzzers and announcements
	There's a lot of ancillery behaviors here, likely tied to 'door open' logic.
	I may not include these but they're likely triggered without altering behavior

# Freight Mode
	This would be a mode you could use to move goods from one floor to another without servicing other users.
	For example, Greg's moving 3 fridges to new apartments on the 10th floor. He might put the elevator into freight mode and then select where he wants to go. The elevator would ignore all outside floor requests (no room)
	I think this is outside of the scope of this assignment.

# Emergency Mode
	During a fire, "Don't use the Elevators!". This is potentially more complex than one might imagine. If a user are currently travelling up to floor 7, we can't exactly trap them in the towering inferno,
	So we would need a mechanism to take them either to the nearest floor or to the ground floor. Not sure which they do in practice, ground floor likely.
	This might be outside the scope of this assignment.

# Stop Button
	As far as I've seen this is a standard function. It may bring an elevator to a halt between floor.
	Ideally, this could be seen as an emergency and patch in emergency services to the car's speaker. There would also be a resume button to cancel this state and continue normal operation.
	I think this might also be outside scope, although a part of the MVP. Will see if I have time to implement it!




## Limitations
	I'm not sure if I want to make this an interruptable process. A real world elevator is constantly taking input and updating itself in real time. Is this something I want to design for?
	I will run under the idea that the ground floor is not L1 - I know this is a country dependant thing but I think this is simpler. (Tests kinda infer that L1 and Ground differ)
	A real world elevator is likely running on something like PLC, simplicity and robustness would be absolutely key to safety.




## Terminology
	I think a shared language is rediculously useful in any system. Interchanging Floor and Level just adds mental load and possible confusion. Domain Driven Design habits.
	So as part of my exploration of any problem space, I like the make these things clear. I'll probably forget.

	Passenger (Ok, I will stop using 'user!'. Maybe.)
	Floor (I think I prefer this to 'Level' as it's more specific)
	Elevator (In place of Lift)
	Summon (The action of pressing an 'up/down' button on a given floor)



## Components/Entities.

Floor
ElevatorCar
ElevatorStateManager			-- Probably be the brains of the operation.

Components on each floor
	FloorSensor
	DirectionSelector
	CurrentFloorIndicator		-- Doubt this is worth keeping updated since we're outputing state/event text.
	ElevatorDirectionIndicator	-- Same with this. 

Components in the elevator
	FloorSelectorPad
	CurrentFloorIndicator		-- Same with this. 
	ElevatorDirectionIndicator	-- Same with this. 



## Notes

	I realise I might be over analysing here but I think it's an important step and a big part of my process. Also, I would like to demonstrate an attention to details. Sorry :)

	I don't think I'll start with the tests here (TDD) as I would like the opportunity to consider the domain language before writing tests.

	I'll stick to a text output of the events, no delays for the sakes of testing and simplicity. (Changed this using Events as they're nicer to test against)

	I have the option of using events or callback to simulate the realtime nature but I think it's easiest just to use something similar to a message pump. I'll just call a Next() function to go to the next step.


## Start

	Probably used an hour up to this point before writing any code. Better get cracking.
	...
	At about the 2-3 hour mark now, i've refactored a few times bbut settled on a more simple structure without involving a complicated state engine. Using Events to report state changes to tests.
	...
	Probably pretty close to the 4 hour mark here and I've still need to define the last 3 tests
	Have noticed a bug at this stage where an elevator travelling up will stop for a passenger wanting to go down - I don't think this is correct behavior.
	...
	fixed the bug and it works across the remaining tests. May have gone a bit over the 4 (4.5?) hours after putting in comments. 