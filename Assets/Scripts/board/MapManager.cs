using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{

	public Tilemap floor;
    private Color originalColor;

	[NonSerialized]
	public int Z_WOLRD_POS = -1;
	[NonSerialized]
	public int FLOOR_TILE_POS = 0;

    // Use this for initialization
	public MapManager(Tilemap floor)
	{
		this.floor = floor;
		originalColor = floor.GetComponent<Tilemap>().color;
		for (int x = floor.cellBounds.xMin; x < floor.cellBounds.xMax; x++) {
            for (int y = floor.cellBounds.yMin; y < floor.cellBounds.yMax; y++)
            {
                floor.RemoveTileFlags(new Vector3Int(x, y, floor.cellBounds.z), TileFlags.LockColor);
            }
        }
	}

	public void changeTileColor(Vector3Int tilePos, Color color)
    {      
		floor.SetColor(new Vector3Int(tilePos.x, tilePos.y, FLOOR_TILE_POS), color);
    }

	public void resetColor(Vector3Int tilePos) {
		floor.SetColor(tilePos, originalColor);
	}

	public Vector3 getWorldPosition(Vector3Int cellPos) {
		Vector3 pos = floor.CellToWorld(cellPos);
		pos.x += floor.cellSize.x / 2;
		pos.y += floor.cellSize.y / 2;
		pos.z = Z_WOLRD_POS;
		return pos;
	}

	public Vector3Int getTilePosition(Vector3 pos) {
		return floor.WorldToCell(pos);
	}

	public int getTileDistance(Vector3Int pos1, Vector3Int pos2) {
		return Math.Abs(pos1.x - pos2.x) + Math.Abs(pos1.y - pos2.y);
	}
    
	public List<Vector3Int> getFloorTiles() {
		BoundsInt bounds = floor.cellBounds;

		List<Vector3Int> tiles_pos = new List<Vector3Int>();

		for (int x = bounds.xMin; x < bounds.size.x; x++) {
			for (int y = bounds.yMin; y < bounds.size.y; y++) {
				Vector3Int tile_pos = new Vector3Int(x, y , FLOOR_TILE_POS);
				if (floor.GetTile(tile_pos) != null) {
					tiles_pos.Add(new Vector3Int(x, y, FLOOR_TILE_POS));
                }
            }
        }
		return tiles_pos;
	}

}
