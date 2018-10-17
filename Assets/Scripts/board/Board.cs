
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Board : MonoBehaviour
{

	MapManager mapManager;
	public GameController gameController;
	public Tilemap floor;

	[NonSerialized]
	private Nullable<Vector3Int> selectedTile;

	[NonSerialized]
    public Vector3Int mouse_over_tile;

	Vector3Int selectedTilePos;

	// Use this for initialization
	void Start () {
		mapManager = new MapManager(floor);
	}
	
	// Update is called once per frame

	public int getTileDistance(Vector3Int pos1, Vector3Int pos2) {
		return this.mapManager.getTileDistance(pos1, pos2);
	}
    
	public void resetTileColor(Vector3Int tile_to_color) {
		mapManager.resetColor(tile_to_color);
	}

	public void changeTileColor(Vector3Int tile_to_color, Color color) {
		mapManager.changeTileColor(tile_to_color, color);
	}

	public void resetBoardColor()
    {
        foreach (Vector3Int tile in getTiles())
        {
            resetTileColor(tile);
        }
    }

	public void onFloorClicked(Vector3 pos) {
		Vector3Int tile_pos = mapManager.getTilePosition(pos);
		gameController.onTileClicked(tile_pos);
	}
   	public void onOverFloor(Vector3 pos) {
		Vector3Int tile_pos = mapManager.getTilePosition(pos);
		gameController.onOverBoard(tile_pos);
	}

	public void addUnitOnBoard(GameObject game_object, Vector3Int tilepos) {
		game_object.transform.position = mapManager.getWorldPosition(tilepos);
		Unit unit = game_object.GetComponent<Unit>();
		unit.tile_position = tilepos;
	}
    
	public List<Vector3Int> getTiles() {
		return mapManager.getFloorTiles();
	}

	public MapManager MapManager
    {
        get
        {
            return mapManager;
        }
    }

	public void selectTile(Vector3Int tile) {
		selectedTile = tile;
	}

	public void unselectTile() {
		selectedTile = null;
	}

	public Nullable<Vector3Int> getSelectedTile() {
		return selectedTile;
	}

	public Vector3 tileToWorldPosition(Vector3Int tilepos) {
		return mapManager.getWorldPosition(tilepos);
	}

}
