
using UnityEngine;

public class GameController : MonoBehaviour {

	public MapManager mapManager;
	public UnitManager unitManager;
	public Color overColor;

	public GameObject unit;

	Vector3Int selectedTilePos;
    
	// Use this for initialization
	void Start () {
		unitManager.addUnit(new Vector3Int(0, 0, 0), unit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
	public void onOverTile(Vector3Int tilePos) {
		mapManager.resetColor(selectedTilePos);
		selectedTilePos = tilePos;
		mapManager.changeTileColor(tilePos, overColor);
	}

	public void onTileSelected(Vector3Int tilepos) {
		Debug.Log(tilepos);
	}
}
