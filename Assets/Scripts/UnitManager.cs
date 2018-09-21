using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	Dictionary<Vector3Int, GameObject> unit_dictionary;
	public MapManager mapManager;


	// Use this for initialization
	void Start () {
		unit_dictionary = new Dictionary<Vector3Int, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addUnit(Vector3Int tile_pos, GameObject unit) {
		unit_dictionary.Add(tile_pos, unit);
		unit.transform.position = mapManager.getWorldPosition(tile_pos);
	}

	public bool containtsUnit(Vector3Int unit_pos) {
		return unit_dictionary.ContainsKey(unit_pos);
	}

	public void displayUnit(Vector3Int unit_pos) {
		Debug.Log(unit_dictionary[unit_pos]);
	}


}
