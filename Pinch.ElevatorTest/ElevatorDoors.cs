using Pinch.ElevatorTest.Events;

namespace Pinch.ElevatorTest;

public class ElevatorDoors
{
    private bool currentlyOpen;

    // Good to be specific with bools rather than just Open and !Open.
    public bool AreOpen => currentlyOpen;
    public bool AreClosed => !currentlyOpen;

    // I won't handle intermediate states such as 'opening' and 'closing', but they're important in real world scenarios.
    public void Open()
    {
        currentlyOpen = true;
    }

    public void Close()
    {
        currentlyOpen = false;
    }
}
