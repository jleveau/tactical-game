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

	public List<Type> getAvailableActions(Unit unit, Board board) {
		return availableActionsTypes;
	}

	public List<Vector3Int> getAvailableTargetsForAction(Unit unit, Board board, Type action) {
		Action my_action = (Action)Activator.CreateInstance(action, unit, board);
		return my_action.getAvailableTargets(unit, board);

	}
}
