


using System.Collections.Generic;
using UnityEngine;
using System;
public class TileMapElement  {

    public Vector3Int tile_pos;
    public GameObject gameobject;
    public List<Vector3> path;
	public MapManager mapManager;

    public int TRANSLATE_SPEED = 1; 
    public TileMapElement(GameObject gameobject, MapManager mapManager, Vector3Int position) {
        this.tile_pos = position;
        this.gameobject = gameobject;
        this.mapManager = mapManager;
        this.gameobject.transform.position = mapManager.getWorldPosition(position);
    }

    public void setPath(List<Vector3Int> tile_path) {
        List<Vector3> path = new List<Vector3>();
		foreach (Vector3Int tile_pos in tile_path) {
			path.Add(mapManager.getWorldPosition(tile_pos));
		}
        this.path = path;
    }

    public void move() {
        if (this.path == null) {
            return;
        }
        if (this.path.Count > 0) {
            Vector3 dest = this.path[0];
            float dx = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameobject.transform.position.x - dest.x));
            float dy = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameobject.transform.position.y - dest.y));
            if (dest.x < this.gameobject.transform.position.x) {
                dx *= -1;
            } 
            if (dest.y < this.gameobject.transform.position.y) {
                dy *= -1;
            }

            this.gameobject.transform.Translate(new Vector3(dx, dy, 0));

            if (this.gameobject.transform.position.x == path[0].x && this.gameobject.transform.position.y == path[0].y) {
                this.path.RemoveAt(0);
            }
        }
    }

}