using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp.Assets.Scripts.TileMapElements.Units;


public class Unit : MonoBehaviour
{
	public Profile profile;
	[NonSerialized]
	public Vector3Int tile_position;

	public float TRANSLATE_SPEED = 0.5F;
	private float EPSILON_DISTANCE = 0.00000000001F;
	private LinkedList<Vector3> current_path;
	private Board boardController;

	void Start()
	{
		current_path = new LinkedList<Vector3>();
	}
	
	// Update is called once per frame
	void Update()
	{
		move();
	}
	
	public void setPath(LinkedList<Vector3Int> tile_path) {
		current_path = new LinkedList<Vector3>();
		foreach (Vector3Int tile_pos in tile_path)
		{
			current_path.AddLast(boardController.MapManager.getWorldPosition(tile_pos));
		}
	}

	public void move() {
		if (this.current_path.First == null || this.current_path.Count == 0)
		{
			return;
		}

		Vector3 dest = this.current_path.First.Value;

		float dx = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameObject.transform.position.x - dest.x));
		float dy = Math.Min(this.TRANSLATE_SPEED, Math.Abs(this.gameObject.transform.position.y - dest.y));
		if (dest.x < this.gameObject.transform.position.x)
		{
			dx *= -1;
		}
		if (dest.y < this.gameObject.transform.position.y)
		{
			dy *= -1;
		}
		this.gameObject.transform.Translate(new Vector3(dx, dy, 0));

		if (Math.Abs(this.gameObject.transform.position.x - dest.x) < EPSILON_DISTANCE && Math.Abs(this.gameObject.transform.position.y - dest.y) < EPSILON_DISTANCE)
		{
			this.tile_position = boardController.MapManager.getTilePosition(dest);
			this.current_path.RemoveFirst();
		}
		
	}

	public void setBoardController(Board boardController) {
		this.boardController = boardController;
	}

    
}
