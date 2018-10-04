

using System;

public class GameControllerActionObserver : IActionObserver
{

    GameController controller;

    public GameControllerActionObserver()
    {
    }

    public GameControllerActionObserver(GameController controller)
    {
        this.controller = controller;
    }

    public void NotifyActionFinished(Unit unit, Action action)
    {
        controller.startSpectateMode();
    }

    public void NotifyActionStarted(Unit unit, Action action)
    {
        controller.endSpectateMode();
    }
}

