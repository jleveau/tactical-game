


using System.Collections.Generic;
using UnityEngine;

class TileMapElement  {

    public Vector2Int position;
    public GameObject gameobject;
    public Vector3Int dest;
    public int TRANSLATE_SPEED = 1; 
    public TileMapElement(GameObject gameobject, Vector2Int position) {
        this.position = position;
        this.gameobject = gameobject;
    }

    public void followPath(List<Vector3Int> new_tile_pos) {
        foreach(Vector3Int pos in new_tile_pos) {
            setDestination(pos);
        }
    }

    public void setDestination(Vector3Int dest) {
       this.dest = dest;
    }

}