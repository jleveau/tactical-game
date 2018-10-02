using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Board board;
	public MenuController menu_controller;
	public List<GameObject> units;



	public Color moveable_tiles_color;
	public Color over_color;

	ActionManager actionManager;
	UnitManager unitManager;
	GameObject currentUnit;

	Vector3Int mouse_over_tile;

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
		StartCoroutine("DisplayTiles");

	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator DisplayTiles() {
		for (; ;)
        {
			board.resetBoardColor();
			displayMovableTiles();
			board.changeTileColor(mouse_over_tile, over_color);      

			yield return null;
		}
	}



	public void displayMovableTiles() {
		
        if (currentUnit != null)
        {
            Unit unit = currentUnit.GetComponent<Unit>();
            List<Vector3Int> moveable_tiles = actionManager.getAvailableMoveActionTarget(unit, board);
            foreach (Vector3Int targetable_position in moveable_tiles)
            {
                board.changeTileColor(targetable_position, moveable_tiles_color);
            }
        }
	}   

	public void onTileClicked(Vector3Int tilepos) {
		Unit unit = currentUnit.GetComponent<Unit>();
		List<Action> actions = actionManager.getAvailableActionsForTarget(unit, tilepos, board);
		menu_controller.displayActionMenu(board.tileToWorldPosition(tilepos), actions);
		                                  
	}

	public void onOverBoard(Vector3Int tilepos) {
		board.resetTileColor(mouse_over_tile);
		mouse_over_tile = tilepos;
	}


}
