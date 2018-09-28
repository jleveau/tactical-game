
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{

	private MapManager mapManager;
	public GameController gameController;
	public Tilemap floor;

	Vector3Int selectedTilePos;

	// Use this for initialization
	void Start () {
		mapManager = new MapManager(floor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getTileDistance(Vector3Int pos1, Vector3Int pos2) {
		return this.mapManager.getTileDistance(pos1, pos2);
	}
    

	public void resetTileColor(Vector3Int tile_to_color) {
		mapManager.resetColor(tile_to_color);
	}

	public void changeTileColor(Vector3Int tile_to_color, Color color) {
		mapManager.changeTileColor(tile_to_color, color);
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
		unit.setBoardController(this);
	}
    
	public void moveUnitTo(GameObject game_object, Vector3Int dest_tilepos) {
		Unit unitComponent = game_object.GetComponent<Unit>();
		unitComponent.setPath(mapManager.getPath(unitComponent.GetComponent<Unit>().tile_position, dest_tilepos));
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

}
