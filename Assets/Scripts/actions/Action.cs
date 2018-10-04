
using System.Collections.Generic;
using UnityEngine;

public abstract class Action  {

    
	MoveAction controller;
       
	public List<Vector3Int> getAvailableTargets(Unit performer, GameController gameController) {
		List<Vector3Int> positions = new List<Vector3Int>();
		foreach (Vector3Int pos in gameController.board.getTiles()) {
			if (condition(performer, pos, gameController)) {
				positions.Add(pos);
			}
		}
		return positions;
	}

    //Condition to perform the action
	public abstract bool condition(Unit performer, Vector3Int target, GameController gameController);
    
    //What does the action do
	public abstract void perform(Unit performer, Vector3Int target, GameController gameController);

	public abstract string getActionText();
}
