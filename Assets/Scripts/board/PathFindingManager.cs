using AssemblyCSharp.Assets.Scripts.algorithm;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager {
    private GraphPathSolver pathSolver;
    private Board board;

    public PathFindingManager() {
        pathSolver = new GraphPathSolver();
    }

    public LinkedList<Vector2Int> getPath(Vector3Int start, Vector3Int end) {
        return pathSolver.getPathForPosition(new Vector2Int(end.x, end.y));
	}

    public void update() {
        this.computeCostMap();
    }

    private void computeCostMap() {
        BoundsInt bounds = board.MapManager.floor.cellBounds;

        int x_size = bounds.xMax - bounds.xMin;
        int y_size = bounds.yMax - bounds.yMax;

        int[][] costs = new int[x_size][];
        for(int i= 0; i< x_size; ++i) {
            costs[i] = new int[y_size];
        }
        for (int i=0; i<x_size; ++i) {
            for(int j = 0; j < y_size; ++j) {
                costs[i][j] = -1;
            }
        }
        updateCostForFloor(costs);
        updateCostForTilesWithEnnemyUnits(costs);
        this.pathSolver.build(costs);
    }

    private void updateCostForFloor(int[][] costs) {
        foreach(Vector3Int tile in this.board.getTiles()) {
            costs[tile.x][tile.y] = 1;
        }
    }

    private void updateCostForTilesWithEnnemyUnits(int[][] costs) {
        foreach(Unit unit in this.board.gameController.unitManager.units) {
            Vector3Int tilepos = unit.tile_position;
            costs[tilepos.x][tilepos.y] = -1;
        }
    }
}