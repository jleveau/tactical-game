using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	List<TileMapElement> units;
	public MapManager mapManager;


	// Use this for initialization
	void Start () {
		units = new List<TileMapElement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addUnit(Vector3Int tile_pos, GameObject unit) {
		TileMapElement element = new TileMapElement(unit, new Vector2Int(tile_pos.x, tile_pos.y));
		units.Add(element);
		unit.transform.position = mapManager.getWorldPosition(tile_pos);
	}

	public void followPath(TileMapElement unit, List<Vector3Int> new_tile_pos) {
		unit.followPath(new_tile_pos);
	}
}
