
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action {

	[NonSerialized]
	public float TRANSLATE_SPEED = 0.5F;

    float EPSILON_DISTANCE = 0.00000000001F;
    LinkedList<Vector3> current_path;
   

	public override string getActionText()
	{
		return "Move";
	}

	public static bool getCondition(Unit performer, Vector3Int target, GameController controller)
    {
        int move_points = performer.profile.movement_points.value;
        int tile_distance = controller.board.getTileDistance(performer.tile_position, target);
        return tile_distance <= move_points && tile_distance > 0;
    }

	public override bool condition()
	{
		return MoveAction.getCondition(performer, target, controller);
	}

	public override void perform()
	{
		int cost = controller.board.getTileDistance(performer.tile_position, target);
		addObserver(controller.actionObserver);

		StartCoroutine("MoveUnit");
		performer.profile.movement_points.value -= cost;
	}

	IEnumerator MoveUnit()
    {
		current_path = new LinkedList<Vector3>();
		LinkedList<Vector3Int> tile_path = controller.board.MapManager.getPath(performer.tile_position, target);
        foreach (Vector3Int tile_pos in tile_path)
        {
			current_path.AddLast(controller.board.MapManager.getWorldPosition(tile_pos));
        }

		NotifyActionStarted();

        for (; ; )
        {
			if (current_path.First == null || current_path.Count == 0)
            {
				yield break;
            }
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
			performer.gameObject.transform.Translate(new Vector3(dx, dy, 0));

			if (Math.Abs(performer.gameObject.transform.position.x - dest.x) < EPSILON_DISTANCE && Math.Abs(performer.gameObject.transform.position.y - dest.y) < EPSILON_DISTANCE)
            {
				performer.tile_position = controller.board.MapManager.getTilePosition(dest);
                current_path.RemoveFirst();
                if (current_path.Count == 0)
                {
					NotifyActionFinished();
                }
            }
            yield return null;
        }
    }
       
}
