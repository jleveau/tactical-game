using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.algorithm;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

	public GameController gameController;

	public Tilemap floor;
	private GridPathSolver pathSolver;
    Color originalColor;

    // Use this for initialization

    void Start()
    {
		pathSolver = new GridPathSolver();
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
		gameController.onTileSelected(worldToCell(Input.mousePosition));
    }

	public void changeTileColor(Vector3Int tilePos, Color color)
    {
		floor.SetColor(tilePos, color);
    }
    
	public void resetColor(Vector3Int cellPos) {
		floor.SetColor(cellPos, originalColor);
	}
    
	public void onOverFloor(Vector3 pos)
    {
		Vector3Int cellPos = worldToCell(pos);
		gameController.onOverTile(cellPos);
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

	private Vector3Int worldToCell(Vector3 pos) {
		return floor.WorldToCell(Camera.main.ScreenToWorldPoint(pos));
	}
    

}
