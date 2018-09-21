using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapManager : MonoBehaviour {

	public Color mouseOverColor;
	Color originalColor;

	Vector3Int selectedTilePosition;

	public Tilemap tilemap;
	// Use this for initialization
	void Start () {
		originalColor = tilemap.GetComponent<Tilemap>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseOver()
    {
		tilemap.SetColor(selectedTilePosition, originalColor);
		selectedTilePosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		tilemap.SetColor(selectedTilePosition, mouseOverColor);
    }

}
