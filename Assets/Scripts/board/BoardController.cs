
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{

	private MapManager mapManager;
	private UnitManager unitManager;

	public Color overColor;
	public Tilemap floor;
	public List<GameObject> units;

	Vector3Int selectedTilePos;
	private GameObject currentUnit;
   
	// Use this for initialization
	void Start () {
		mapManager = new MapManager(floor);
        unitManager = new UnitManager();
		int i = 0;
		foreach(GameObject unit in units) {
			addUnitOnBoard(unit, new Vector3Int(0, i, -1));
	        ++i;
		}
		currentUnit = unitManager.getNextUnit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
	public void onOverFloor(Vector3 pos) {
		Vector3Int tile_pos = mapManager.getTilePosition(pos);
		mapManager.resetColor(selectedTilePos);
		selectedTilePos = tile_pos;
		mapManager.changeTileColor(tile_pos, overColor);
	}

	public void onFloorClicked(Vector3 pos) {
		Vector3Int tile_pos = mapManager.getTilePosition(pos);
		moveUnitTo(currentUnit, tile_pos);
	}
   
	private void addUnitOnBoard(GameObject game_object, Vector3Int tilepos) {
		game_object.transform.position = mapManager.getWorldPosition(tilepos);
		Unit unit = game_object.GetComponent<Unit>();
		unit.tile_position = tilepos;
		unit.setBoardController(this);
		unitManager.addUnit(game_object);
	}
    
	private void moveUnitTo(GameObject game_object, Vector3Int dest_tilepos) {
		Unit unitComponent = game_object.GetComponent<Unit>();
		unitComponent.setPath(mapManager.getPath(unitComponent.GetComponent<Unit>().tile_position, dest_tilepos));
		currentUnit = unitManager.getNextUnit();
	}

	private GameObject nextUnit()
	{
		return unitManager.getNextUnit();
	}

	public MapManager MapManager
    {
        get
        {
            return mapManager;
        }
    }

	public UnitManager UnitManager
    {
        get
        {
            return unitManager;
        }
    }

}
