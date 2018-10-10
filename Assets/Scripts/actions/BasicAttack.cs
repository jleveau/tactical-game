
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class BasicAttack : Action
    {
		public override bool condition()
		{
			return 
				performer.profile.action_points.value >= 1 &&
				controller.board.getTileDistance(performer.tile_position, target) == 1 &&
				controller.unitManager.getUnitForTile(target) != null;
		}

		public override string getActionText()
		{
			return "Attack";
		}

		public override void perform()
		{
            addObserver(controller.actionObserver);
            StartCoroutine("AttackUnit");
        }

	    public static bool getCondition(Unit performer, Vector3Int target, GameController controller)
    	{
            return performer.profile.action_points.value > 0;    	
        }

        IEnumerator AttackUnit()
        {
			NotifyActionStarted();
            Unit target_unit = controller.unitManager.getUnitForTile(this.target);
            int damages = performer.profile.attack.value;
            performer.inflictDamage(damages, target_unit);
			performer.profile.action_points.value -= 1;
			NotifyActionFinished();
            yield break;
            
        }
	}
