using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	public Board board;
	public MenuController menu_controller;
	public UnitManager unitManager;

	public Color moveable_tiles_color;
	public Color over_color;
	public Color selected_tile_color;

	public GameControllerActionObserver actionObserver;
	public ActionManager actionManager;
	[NonSerialized]
	public bool spectateMode;
   
   
	void Awake()
	{
		actionObserver = new GameControllerActionObserver(this);      
	}


	// Use this for initialization
	void Start () {

		unitManager.createDebugUnit(new Vector3Int(-5, 0, 0));
		unitManager.createDebugUnit(new Vector3Int(5, 0, 0));

		StartCoroutine("DisplayTiles");
		nextTurn();
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
		Unit unit = unitManager.CurrentUnit;
		if (unit != null)
        {
			LinkedList<Vector3Int> moveable_tiles = this.board.getTilesInMoveRange(unit.tile_position, unit.Profile.movement_points.value);
            foreach (Vector3Int targetable_position in moveable_tiles)
            {
                board.changeTileColor(targetable_position, moveable_tiles_color);
            }
        }
	}   
    
	public void onTileClicked(Vector3Int tilepos) {
		if (!spectateMode) {
			Unit unit = unitManager.CurrentUnit;
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
        //Hide old UI
		menu_controller.closeActionMenu();
        board.unselectTile();

        //Update Units
		unitManager.updateForNextTurn();      
		unitManager.nextUnit();

		menu_controller.displayProfileMenu(unitManager.CurrentUnit.profile);
   	}

}