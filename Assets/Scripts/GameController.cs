using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {

	public Board board;
	public List<GameObject> units;
	public Color target_action_color;
	ActionManager actionManager;
	UnitManager unitManager;
	GameObject currentUnit;

	// Use this for initialization
	void Start () {
		actionManager = new ActionManager();
		unitManager = new UnitManager();
		int i = 0;
		foreach(GameObject unit in units) {
			board.addUnitOnBoard(unit, new Vector3Int(0, i, -1));
			unitManager.addUnit(unit);
	        ++i;
		}
		currentUnit = unitManager.getNextUnit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onTileClicked(Vector3Int tilepos) {
		if (currentUnit != null) {
			Unit unit = currentUnit.GetComponent<Unit>();
			List<Type> action_types = actionManager.getAvailableActions(unit, board);
			Type action_type = action_types[0];

			List<Vector3Int> targetable_positions = actionManager.getAvailableTargetsForAction(unit, board, action_type);
			foreach(Vector3Int targetable_position in targetable_positions) {
				board.changeTileColor(targetable_position, target_action_color);
			}
		}
	}

	public void onOverBoard(Vector3Int tilepos) {

	}
}
