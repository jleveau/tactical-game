


using System.Collections.Generic;
using UnityEngine;
using System;
public class TileMapElement  {

    public Vector3Int tile_position;
    public GameObject gameobject;
	public LinkedList<Vector3> path;
	public MapManager mapManager;

	public float TRANSLATE_SPEED = 0.5F;
	public float EPSILON_DISTANCE = 0.00000000001F;

    public TileMapElement(GameObject gameobject, MapManager mapManager, Vector3Int position) {
		this.tile_position = position;
        this.gameobject = gameobject;
        this.mapManager = mapManager;
        this.gameobject.transform.position = mapManager.getWorldPosition(position);
		this.path = new LinkedList<Vector3>();
    }

	public void setPath(LinkedList<Vector3Int> tile_path) {
		LinkedList<Vector3> path_world_pos = new LinkedList<Vector3>();
		foreach (Vector3Int tile_pos in tile_path) {
			path_world_pos.AddLast(mapManager.getWorldPosition(tile_pos));
		}
		path = path_world_pos;
    }

    public void move() {
		if (this.path.First == null) {
            return;
        }
        if (this.path.Count > 0) {
            Vector3 dest = this.path.First.Value;

            float dx = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameobject.transform.position.x - dest.x));
            float dy = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameobject.transform.position.y - dest.y));
            if (dest.x < this.gameobject.transform.position.x) {
                dx *= -1;
            } 
            if (dest.y < this.gameobject.transform.position.y) {
                dy *= -1;
            }
            this.gameobject.transform.Translate(new Vector3(dx, dy, 0));

			if (Math.Abs(this.gameobject.transform.position.x - dest.x) < EPSILON_DISTANCE && Math.Abs(this.gameobject.transform.position.y - dest.y) < EPSILON_DISTANCE) {
                this.tile_position = this.mapManager.floor.WorldToCell(dest);

                this.path.RemoveFirst();
            }
        }
    }
}