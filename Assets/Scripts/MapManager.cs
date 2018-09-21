using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

	public GameController gameController;

	public Tilemap floor;

    Color originalColor;

    // Use this for initialization

    void Start()
    {
		originalColor = floor.GetComponent<Tilemap>().color;
		for (int x = floor.cellBounds.xMin; x < floor.cellBounds.xMax; x++) {
			for (int y = floor.cellBounds.yMin; y < floor.cellBounds.yMax; y++) {
				floor.RemoveTileFlags(new Vector3Int(x, y, floor.cellBounds.z), TileFlags.LockColor);
			}
		}
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			selectCell();
        }
    }

	public void selectCell()
    {
		gameController.onTileSelected(cellPosToTilePos(worldToCell(Input.mousePosition)));
    }

	public void changeTileColor(Vector3Int tilePos, Color color)
    {
		Debug.Log("in");
		Debug.Log(color);
		floor.SetColor(tilePosToCellPos(tilePos), color);
    }

	public void resetColor(Vector3Int tilePos) {
		floor.SetColor(tilePosToCellPos(tilePos), originalColor);
	}
    
	public void onOverFloor(Vector3 pos)
    {
		Vector3Int cellPos = worldToCell(pos);
		gameController.onOverTile(cellPosToTilePos(cellPos));
    }

	public Vector3 getWorldPosition(Vector3Int tilePos) {
		Vector3 pos = floor.CellToWorld(tilePosToCellPos(tilePos));
		pos.x += floor.cellSize.x / 2;
		pos.y += floor.cellSize.y / 2;
		return pos;
	}

	private Vector3Int worldToCell(Vector3 pos) {
		return floor.WorldToCell(Camera.main.ScreenToWorldPoint(pos));
	}

	private Vector3Int cellPosToTilePos(Vector3Int cellPos) {
		Vector3Int bounds = floor.origin;
		int PosX = cellPos.x - bounds.x;
		int PosY = cellPos.y - bounds.y;

		return new Vector3Int(PosX, PosY, cellPos.z);
	}

	private Vector3Int tilePosToCellPos(Vector3Int tilePos)
    {
		Vector3Int bounds = floor.origin;
		int PosX = tilePos.x + bounds.x;
		int PosY = tilePos.y + bounds.y;

		return new Vector3Int(PosX, PosY, tilePos.z);
    } 

}
