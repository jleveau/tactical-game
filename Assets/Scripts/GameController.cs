using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	public Board board;
	public MenuController menu_controller;
	public List<GameObject> units;   

	public Color moveable_tiles_color;
	public Color over_color;
	public Color selected_tile_color;

	public GameControllerActionObserver actionObserver;
	public ActionManager actionManager;

	public bool spectateMode;

	UnitManager unitManager;

	// Use this for initialization
	void Start () {
		unitManager = new UnitManager();
		actionObserver = new GameControllerActionObserver(this);

		int i = 0;
		foreach(GameObject unit in units) {
			board.addUnitOnBoard(unit, new Vector3Int(0, i, -1));
			unitManager.addUnit(unit.GetComponent<Unit>());
	        ++i;
		}

		nextTurn();
		StartCoroutine("DisplayTiles");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator DisplayTiles() {
		for (; ;)
        {
			board.resetBoardColor();
			if (!spectateMode) {
				displayMovableTiles();
				board.changeTileColor(board.mouse_over_tile, over_color);
				Nullable<Vector3Int> selectedTile = board.getSelectedTile();
                if (selectedTile != null)
                {
                    board.changeTileColor(selectedTile.GetValueOrDefault(), selected_tile_color);
                }
			}         
			yield return null;
		}
	}

	public void displayMovableTiles() {
		Unit unit = unitManager.currentUnit;
		if (unit != null)
        {
			List<Vector3Int> moveable_tiles = new List<Vector3Int>();
			foreach(Vector3Int pos in board.getTiles()) {
				if (MoveAction.getCondition(unitManager.currentUnit, pos, this)) {
					moveable_tiles.Add(pos);
				}
			}
            foreach (Vector3Int targetable_position in moveable_tiles)
            {
                board.changeTileColor(targetable_position, moveable_tiles_color);
            }
        }
	}   
    
	public void onTileClicked(Vector3Int tilepos) {
		if (!spectateMode) {
			Unit unit = unitManager.currentUnit;
            List<Action> actions = actionManager.getAvailableActionsForTarget(unit, tilepos);
            menu_controller.displayActionMenu(actions);
            board.selectTile(tilepos);                
		}              
	}

	public void onOverBoard(Vector3Int tilepos) {
		if (!spectateMode)
		{
			board.resetTileColor(board.mouse_over_tile);
			board.mouse_over_tile = tilepos;
		}
	}

	public void selectAction(Action action) {
		if (board.getSelectedTile()!= null) {
			action.perform();
			menu_controller.closeActionMenu();
			board.unselectTile();
		}      
	}

	public void nextTurn() {
		unitManager.updateForNextTurn();
		unitManager.nextUnit();
		menu_controller.closeActionMenu();
		board.unselectTile();
	}
   

}
