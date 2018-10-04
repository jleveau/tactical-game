using System;
using System.Collections.Generic;
using UnityEngine;


public class ActionManager 
{
	private List<Type> availableActionsTypes;
	private GameController gameController;

	public ActionManager(GameController controller)
	{
		availableActionsTypes = new List<Type>();
		availableActionsTypes.Add(typeof(MoveAction));
		availableActionsTypes.Add(typeof(PassAction));
		this.gameController = controller;

	}
       
	public List<Action> getAvailableActionsForTarget(Unit unit, Vector3Int position) {
		List<Action> actions = new List<Action>();
		foreach (Type action_type in availableActionsTypes) {
			Action my_action = (Action)Activator.CreateInstance(action_type);
			if (my_action.condition(unit, position, gameController)) {
				actions.Add(my_action);
			}
		}
		return actions;
	}

	public List<Vector3Int> getAvailableTargetsForAction(Unit unit, Type action) {
		Action my_action = (Action)Activator.CreateInstance(action);
		return my_action.getAvailableTargets(unit, gameController);

	}

	public List<Vector3Int> getAvailableMoveActionTarget(Unit unit) {
		return getAvailableTargetsForAction(unit, typeof(MoveAction));
	}
}
