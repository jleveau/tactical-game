
using System.Collections.Generic;
using UnityEngine;

	public class MoveActionRangeManager
    {


    	static LinkedList<Vector3Int> reachable_tiles;   
    	public Board board;  

    	public static void reset()
    	{
    		reachable_tiles = null;
    	}

	public static LinkedList<Vector3Int> getReachableTiles(Vector3Int pos, int range, Board board) {
    		if (reachable_tiles == null) {
    			reachable_tiles = board.getTilesInMoveRange(pos, range);
    		}
    		return reachable_tiles;      
    	}
    }
