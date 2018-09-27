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

	public Action(Unit performer, Vector3Int target, Board board)
	{
		this.performer = performer;
		this.target = target;
		this.board = board;
	}


	public abstract bool condition();
	public abstract void perform();

}
