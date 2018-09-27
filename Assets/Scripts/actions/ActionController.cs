using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.gamephases
{
	public class ActionController 
    {
		public Board board;
		private List<Type> availableActionsTypes;

        public ActionController()
        {
			availableActionsTypes = new List<Type>();
			availableActionsTypes.Add(typeof(MoveAction));
        }

		public List<Action> getAvailableActions(Unit unit) {
			List<Action> actions = new List<Action>(); 
			foreach (Vector3Int tile in board.getTiles()) {
				foreach (Type action_type in availableActionsTypes) {
					Action action = (Action)Activator.CreateInstance(action_type, unit, tile, board);
					if (action.condition()) {
						actions.Add(action);
					}
				}

			}
			return actions;
		}
    }
}
