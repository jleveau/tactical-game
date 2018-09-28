using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.algorithm;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{

	public Tilemap floor;
	private GridPathSolver pathSolver;
    private Color originalColor;

    // Use this for initialization
	public MapManager(Tilemap floor)
	{
		pathSolver = new GridPathSolver();
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
		floor.SetColor(tilePos, color);
    }
    
	public void resetColor(Vector3Int cellPos) {
		floor.SetColor(cellPos, originalColor);
	}
    
   
	public LinkedList<Vector3Int> getPath(Vector3Int start, Vector3Int end) {
		List<Vector2Int> path = pathSolver.FindPath(new Vector2Int(start.x, start.y), new Vector2Int(end.x, end.y));
		LinkedList<Vector3Int> path3D = new LinkedList<Vector3Int>();
		foreach (Vector2Int pos in path) {
			path3D.AddFirst(new Vector3Int(pos.x, pos.y, 0));
		}
		return path3D;
	}

	public Vector3 getWorldPosition(Vector3Int cellPos) {
		Vector3 pos = floor.CellToWorld(cellPos);
		pos.x += floor.cellSize.x / 2;
		pos.y += floor.cellSize.y / 2;
		pos.z = -1;
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
		TileBase[] allTiles = floor.GetTilesBlock(bounds);

		List<Vector3Int> tiles_pos = new List<Vector3Int>();

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
					tiles_pos.Add(new Vector3Int(x, y, -1));
                }
            }
        }
		return tiles_pos;
	}

}
