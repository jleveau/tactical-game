using UnityEngine;


using System;

public class GameControllerActionObserver : IActionObserver
{

    GameController controller;

	public GameControllerActionObserver(GameController controller)
    {
		this.controller = controller;
    }
       
    public void NotifyActionStarted(Unit unit, Action action)
    {
		controller.spectateMode = true;
    }

	public void NotifyActionFinished(Unit unit, Action action)
    {
		controller.spectateMode = false;
    }
}

