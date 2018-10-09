using System;
using UnityEngine;


	public class PassAction : Action
    {


	public override bool condition()
		{
			return true;
		}

		public override string getActionText()
		{
			return "Pass";
		}

		public override void perform()
		{
		    controller.nextTurn();
		}

	    public static bool getCondition(Unit performer, Vector3Int target, GameController controller)
    	{
    		return true;
    	}

	}
