using System;
using UnityEngine;


	public class PassAction : Action
    {

		public override bool condition(Unit performer, Vector3Int target, GameController gameController)
		{
			return true;
		}

		public override string getActionText()
		{
			return "Pass";
		}

		public override void perform(Unit performer, Vector3Int target, GameController gameController)
		{
			gameController.changeCurrentPlayer();
		}
	}
