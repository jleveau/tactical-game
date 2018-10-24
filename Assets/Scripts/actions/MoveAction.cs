
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{

    public float TRANSLATE_SPEED = 0.5F;

    float EPSILON_DISTANCE = 0.00000000001F;
    LinkedList<Vector3> current_path;


    public override string getActionText()
    {
        return "Move";
    }

    public static bool getCondition(Unit performer, Vector3Int target, GameController controller)
    {
		if (target == performer.tile_position) {
			return false;
		}
		int move_points = performer.Profile.movement_points.value;
		LinkedList<Vector3Int> reachables = MoveActionRangeManager.getReachableTiles(performer.tile_position, move_points, controller.board);
		foreach(Vector3Int pos in reachables) {
			if (pos == target){            
				return true;
			}
		}      
		return false;
    }

	public override bool condition()
	{
		return MoveAction.getCondition(performer, target, controller);
	}

	public override void perform()
	{
		addObserver(controller.actionObserver);
		StartCoroutine("MoveUnit");
	}

	IEnumerator MoveUnit()
    {
        current_path = new LinkedList<Vector3>();
        //Get the path to follow
        LinkedList<Vector3Int> tile_path = controller.board.getPath(performer.tile_position, target);

        //Covert the path from tiles position in world position
        foreach (Vector3Int tile_pos in tile_path)
        {
            current_path.AddLast(controller.board.MapManager.getWorldPosition(tile_pos));
        }

		NotifyActionStarted();
		Debug.Log(current_path.Count);
        for (; ; )
        {
            if (current_path.First == null || current_path.Count == 0)
            {
                NotifyActionFinished();
                yield break;
            }
			//Compute translation
            Vector3 dest = current_path.First.Value;
			float dx = Math.Min(TRANSLATE_SPEED, Math.Abs(performer.gameObject.transform.position.x - dest.x));
			float dy = Math.Min(TRANSLATE_SPEED, Math.Abs(performer.gameObject.transform.position.y - dest.y));
			if (dest.x < performer.gameObject.transform.position.x)
            {
                dx *= -1;
            }
			if (dest.y < performer.gameObject.transform.position.y)
            {
                dy *= -1;
            }
			//Apply translation
			performer.gameObject.transform.Translate(new Vector3(dx, dy, 0));

			//Next tile reached
			if (Math.Abs(performer.gameObject.transform.position.x - dest.x) < EPSILON_DISTANCE && Math.Abs(performer.gameObject.transform.position.y - dest.y) < EPSILON_DISTANCE)
            {
				// change unit current tile
				performer.tile_position = controller.board.MapManager.getTilePosition(dest);
				// change the target
                current_path.RemoveFirst();
				// update unit movement points
				performer.Profile.movement_points.value -= 1;
            }
            yield return null;
        }
    }
       
}
