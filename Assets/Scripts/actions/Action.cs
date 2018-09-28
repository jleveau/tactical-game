using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;

public abstract class Action  {

	protected Unit performer;
	protected Vector3Int target;
	protected Board board;

	MoveAction controller;

	public Action(Unit performer, Board board)
	{
		this.performer = performer;
		this.board = board;
	}

	public List<Vector3Int> getAvailableTargets(Unit performer, Board board) {
		List<Vector3Int> positions = new List<Vector3Int>();
		foreach (Vector3Int pos in this.board.getTiles()) {
			if (condition(this.performer, pos, board)) {
				positions.Add(pos);
			}
		}
		return positions;
	}

	public abstract bool condition(Unit performer, Vector3Int target, Board board);

	private bool testCondition(Vector3Int target) {
		return condition(this.performer, target, this.board);
	}
	public abstract void perform(Vector3Int tile);

}
