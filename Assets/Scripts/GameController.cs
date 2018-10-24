using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	public Board board;
	public MenuController menu_controller;
	public UnitManager unitManager;
	public tile_display tile_displayer;
   
	public GameControllerActionObserver actionObserver;
	public ActionManager actionManager;
	public bool spectateMode;
   
	void Awake()
	{
		actionObserver = new GameControllerActionObserver(this);      
	}


	// Use this for initialization
	void Start () {      
		unitManager.createDebugUnit(new Vector3Int(-5, 0, 0));
		unitManager.createDebugUnit(new Vector3Int(5, 0, 0));

		nextTurn();
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
		tile_displayer.notifyTurnChanged();
		MoveActionRangeManager.reset();      

   	}

	public void setSpectate(bool spectateMode) {
		this.spectateMode = spectateMode;
	}

	public void spectateAction(Action action) {
		action.addObserver(this.actionObserver);
	}

}