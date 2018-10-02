
using System;
using UnityEngine;

public class MoveAction : Action {
	   
	public override bool condition(Unit performer, Vector3Int target, Board board)
	{
		return board.getTileDistance(performer.tile_position, target) < performer.profile.movement_points;
	}

	public override void perform(Unit performer, Vector3Int target, Board board)
	{
		board.moveUnitTo(performer.gameObject, target);
	}

	public override string getActionText()
	{
		return "move";
	}

}
