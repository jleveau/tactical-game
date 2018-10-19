using AssemblyCSharp.Assets.Scripts.algorithm;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager {
    GraphPathSolver pathSolver;
	const int UNREACHABLE_COST = 100000;

    public PathFindingManager() {
        pathSolver = new GraphPathSolver();
    }
       
	public LinkedList<Vector3Int> getPath(Vector3Int start, Vector3Int end, GameController controller) {
		BoundsInt boundsInt = controller.board.MapManager.floor.cellBounds;      
		int[][] cost_map = BuildCostMap(controller, boundsInt);
      
		pathSolver.buildPaths(cost_map, new Vector2Int(start.x - boundsInt.xMin, start.y - boundsInt.yMin));

		LinkedList <Vector2Int> path2D = pathSolver.getPathForPosition(new Vector2Int(end.x - boundsInt.xMin, end.y - boundsInt.yMin));      
		LinkedList<Vector3Int> path = new LinkedList<Vector3Int>();

		foreach (Vector2Int cell2D in path2D)
        {
			path.AddLast(new Vector3Int(cell2D.x + boundsInt.xMin, cell2D.y + boundsInt.yMin, controller.board.MapManager.FLOOR_TILE_POS));
        }
		return path;
	}

	public LinkedList<Vector3Int> getReachableTiles(Vector3Int start, int max_cost, GameController controller)
    {
        BoundsInt boundsInt = controller.board.MapManager.floor.cellBounds;
        int[][] cost_map = BuildCostMap(controller, boundsInt);
		pathSolver.buildPaths(cost_map, new Vector2Int(start.x - boundsInt.xMin, start.y - boundsInt.yMin));

        LinkedList<Vector3Int> reachables = new LinkedList<Vector3Int>();

        foreach (Vector2Int cell2D in pathSolver.getReachablePos(max_cost))
        {
            reachables.AddLast(new Vector3Int(cell2D.x + boundsInt.xMin, cell2D.y + boundsInt.yMin, controller.board.MapManager.FLOOR_TILE_POS));
        }
        return reachables;
    }
    
	int[][] BuildCostMap(GameController controller, BoundsInt offset) {
		offset = controller.board.MapManager.floor.cellBounds;

		int[][] cost_map = new int[offset.xMax - offset.xMin][];      
		for (int i = 0; i < offset.xMax - offset.xMin; ++i) {
			cost_map[i] = new int[offset.yMax - offset.yMin];
		}

		for (int i = 0; i < cost_map.Length; ++i) {
			for (int j = 0; j < cost_map[i].Length; ++j) {
				cost_map[i][j] = costForTile(new Vector2Int(i,j), controller);
			}
		}
              
		return cost_map;
	}

	int costForTile(Vector2Int pos, GameController controller) {
		Vector3Int tile_pos = new Vector3Int(pos.x, pos.y, controller.board.MapManager.FLOOR_TILE_POS);

		if (controller.unitManager.getUnitForTile(tile_pos) != null) {
			return UNREACHABLE_COST;
		}
    
		return 1;
	}



}