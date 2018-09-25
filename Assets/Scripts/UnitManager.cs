using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	public List<TileMapElement> units;
	public MapManager mapManager;


	// Use this for initialization
	void Start () {
		units = new List<TileMapElement>();
	}
	
	// Update is called once per frame
	void Update () {
		moveUnits();
	}

	public void addUnit(Vector3Int tile_pos, GameObject unit) {
		TileMapElement element = new TileMapElement(unit, mapManager, tile_pos);
		units.Add(element);
	}

	public void moveUnitTo(TileMapElement unit, Vector3Int tile_pos) {
		List<Vector3Int> path = mapManager.getPath(unit.tile_pos, tile_pos);
		unit.setPath(path);
	}
 
    private void moveUnits() {
        foreach(TileMapElement unit in units) {
			unit.move();
		}
    }
}
