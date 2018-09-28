
using System;
using UnityEngine;

public class MoveAction : Action {
	
   public MoveAction(Unit performer, Board board) : base(performer, board)
	{
	}

	public override bool condition(Unit performer, Vector3Int target, Board board)
	{
		return this.board.getTileDistance(performer.tile_position, target) < performer.profile.movement_points;
	}

	public override void perform(Vector3Int target)
	{
		this.board.moveUnitTo(this.performer.gameObject, target);
	}



    
}
