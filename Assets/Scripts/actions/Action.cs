
using System.Collections.Generic;
using UnityEngine;

public abstract class Action  {

    
	MoveAction controller;
       
	public List<Vector3Int> getAvailableTargets(Unit performer, Board board) {
		List<Vector3Int> positions = new List<Vector3Int>();
		foreach (Vector3Int pos in board.getTiles()) {
			if (condition(performer, pos, board)) {
				positions.Add(pos);
			}
		}
		return positions;
	}

	public abstract bool condition(Unit performer, Vector3Int target, Board board);
    

	public abstract void perform(Unit performer, Vector3Int target, Board board);

	public abstract string getActionText();
}
