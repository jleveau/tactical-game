
using System;
using UnityEngine;

public class MoveAction : Action {

	   
	public override bool condition(Unit performer, Vector3Int target, GameController gameController)
	{
		int move_points = performer.profile.movement_points.current_value;
		int tile_distance = gameController.board.getTileDistance(performer.tile_position, target);
		return tile_distance <= move_points && tile_distance > 0;
	}

	public override void perform(Unit performer, Vector3Int target, GameController gameController)
	{
		gameController.board.moveUnitTo(performer.gameObject, target);
		int cost = gameController.board.getTileDistance(performer.tile_position, target);
		performer.profile.movement_points.current_value -= cost;

		UnitObserver observer = new UnitObserver(gameController);
		performer.addObserver(observer);
	}

	public override string getActionText()
	{
		return "Move";
	}

}
