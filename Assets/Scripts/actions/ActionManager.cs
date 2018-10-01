using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;


public class ActionManager 
{
	private List<Type> availableActionsTypes;

	public ActionManager()
	{
		availableActionsTypes = new List<Type>();
		availableActionsTypes.Add(typeof(MoveAction));
	}
       
	public List<Action> getAvailableActionsForTarget(Unit unit, Vector3Int position, Board board) {
		List<Action> actions = new List<Action>();
		foreach (Type action_type in availableActionsTypes) {
			Action my_action = (Action)Activator.CreateInstance(action_type);
			if (my_action.condition(unit, position, board)) {
				actions.Add(my_action);
			}
		}
		return actions;
	}

	public List<Vector3Int> getAvailableTargetsForAction(Unit unit, Board board, Type action) {
		Action my_action = (Action)Activator.CreateInstance(action);
		return my_action.getAvailableTargets(unit, board);

	}

	public List<Vector3Int> getAvailableMoveActionTarget(Unit unit, Board board) {
		return getAvailableTargetsForAction(unit, board, typeof(MoveAction));
	}
}
