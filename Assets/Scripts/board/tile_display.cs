using System;
using System.Collections.Generic;
using UnityEngine;

public class tile_display : MonoBehaviour {
	public GameController controller;   

	public Color moveable_tiles_color;
    public Color over_color;
    public Color selected_tile_color;

	public LinkedList<Vector3Int> reachable_tiles;

	// Use this for initialization
	void Start () {
		reachable_tiles = new LinkedList<Vector3Int>();
	}

	// Update is called once per frame
	void Update()
	{
		controller.board.resetBoardColor();
		if (!controller.spectateMode)
		{
			displayMovableTiles();
			controller.board.changeTileColor(controller.board.mouse_over_tile, over_color);
			Nullable<Vector3Int> selectedTile = controller.board.getSelectedTile();
			if (selectedTile != null)
			{
				controller.board.changeTileColor(selectedTile.GetValueOrDefault(), selected_tile_color);
			}
		}
	}
    
    void displayMovableTiles() {
		Unit unit = controller.unitManager.CurrentUnit;
        if (unit != null)
        {
			foreach (Vector3Int targetable_position in reachable_tiles)
            {
				controller.board.changeTileColor(targetable_position, moveable_tiles_color);
            }
        }
    }

	public void notifyTurnChanged() {
		Unit unit = controller.unitManager.CurrentUnit;      
		reachable_tiles = controller.board.getTilesInMoveRange(unit.tile_position, unit.Profile.movement_points.value);
	}

	public void notifyActionStarted() {
		Unit unit = controller.unitManager.CurrentUnit;            
		reachable_tiles = controller.board.getTilesInMoveRange(unit.tile_position, unit.Profile.movement_points.value);
	}

	public void notifyActionFinished() {
		Unit unit = controller.unitManager.CurrentUnit;            
		reachable_tiles = controller.board.getTilesInMoveRange(unit.tile_position, unit.Profile.movement_points.value);      
	}

}
