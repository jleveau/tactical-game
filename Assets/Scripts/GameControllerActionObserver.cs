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
		controller.setSpectate(true);
		controller.tile_displayer.notifyActionStarted();      
    }

	public void NotifyActionFinished(Unit unit, Action action)
    {
		controller.setSpectate(false);
		controller.tile_displayer.notifyActionFinished();
		MoveActionRangeManager.reset();
    }
}

