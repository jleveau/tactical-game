
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class BasicAttack : Action
    {
		public override bool condition()
		{
		    return BasicAttack.getCondition(performer, target, controller);
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
		    
		    int action_points = performer.Profile.action_points.value;

            //has action points, and units are in range and their is a unit on the targetted tile
            return action_points >= 1 &&
                controller.board.getTileDistance(performer.tile_position, target) == 1 &&
                controller.unitManager.getUnitForTile(target) != null;
        }

        IEnumerator AttackUnit()
        {
			NotifyActionStarted();

            //Inflict damages
            Unit target_unit = controller.unitManager.getUnitForTile(this.target);
		    int basic_damage = performer.Profile.damages.value;
            performer.inflictDamage(basic_damage, target_unit);

            //Update action points
		    performer.Profile.action_points.value -= 1;

			Animator animator = performer.gameObject.GetComponent<Animator>();
			animator.SetTrigger("Attack");
			animator.SetTrigger("Attack");

			NotifyActionFinished();

            yield break;
            
        }
	}
