using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Board board;
	public MenuController menu_controller;
	public List<GameObject> units;   

	public Color moveable_tiles_color;
	public Color over_color;
	public Color selected_tile_color;

	ActionManager actionManager;
	UnitManager unitManager;

	Vector3Int selectedTile;
	Vector3Int mouse_over_tile;

	// Use this for initialization
	void Start () {
		actionManager = new ActionManager(this.GetComponent<GameController>());
		unitManager = new UnitManager();
		int i = 0;
		foreach(GameObject unit in units) {
			board.addUnitOnBoard(unit, new Vector3Int(0, i, -1));
			unitManager.addUnit(unit.GetComponent<Unit>());
	        ++i;
		}

		changeCurrentPlayer();
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
			board.changeTileColor(selectedTile, selected_tile_color);      

			yield return null;
		}
	}

	public void displayMovableTiles() {
		Unit unit = unitManager.currentUnit;
		if (unit != null)
        {
            List<Vector3Int> moveable_tiles = actionManager.getAvailableMoveActionTarget(unit);
            foreach (Vector3Int targetable_position in moveable_tiles)
            {
                board.changeTileColor(targetable_position, moveable_tiles_color);
            }
        }
	}   

	public void onTileClicked(Vector3Int tilepos) {
		Unit unit = unitManager.currentUnit;
		List<Action> actions = actionManager.getAvailableActionsForTarget(unit, tilepos);
		menu_controller.displayActionMenu(actions);
		selectedTile = tilepos;                                  
	}

	public void onOverBoard(Vector3Int tilepos) {
		board.resetTileColor(mouse_over_tile);
		mouse_over_tile = tilepos;
	}

	public void selectAction(Action action) {
		Unit unit = unitManager.currentUnit;
		action.perform(unit, selectedTile, this);
	}

	public void changeCurrentPlayer() {
		unitManager.nextUnit();
		menu_controller.closeActionMenu();
	}

}
