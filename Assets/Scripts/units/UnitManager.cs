using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour{
    
	List<Unit> units;
	Queue<Unit> turnQueue;

	public GameObject debugUnitPrefab;
	public GameController gameController;
    
	Unit currentUnit;

	public Unit CurrentUnit
    {
        get
        {
            return currentUnit;
        }
    }   

	// Use this for initialization
    void Awake()
    {
		units = new List<Unit>();
        turnQueue = new Queue<Unit>();
    }
    
	public void createDebugUnit(Vector3Int position) {
		GameObject unit_object = Instantiate(debugUnitPrefab);
		gameController.board.addUnitOnBoard(unit_object, position);
		Debug.Log(units);
		units.Add(unit_object.GetComponent<Unit>());
	}
   
    public Unit getUnitForTile(Vector3Int tilepos) {
		foreach(Unit unit in units) {
			if (unit.tile_position.x == tilepos.x && 
				unit.tile_position.y == tilepos.y) {
				return unit;
			}
		}
		return null;
	}

	public void nextUnit() {
		
		if (turnQueue.Count == 0)
        {
            turnQueue = buildTurnQueue();
        }

		if (turnQueue.Count > 0) {
			currentUnit = turnQueue.Dequeue();
		}
    }

	public void updateForNextTurn() {
		foreach (Unit unit in units) {
			unit.new_turn_update();
		}
	}

	Queue<Unit> buildTurnQueue()
    {
		Unit[] turnarray = units.ToArray();
		Array.Sort(turnarray, delegate (Unit obj1, Unit obj2)
		{
			Unit unit1 = obj1.GetComponent<Unit>();
			Unit unit2 = obj2.GetComponent<Unit>();
			int unit1_initiative = unit1.Profile.initiative.value;
			int unit2_initiative = unit2.Profile.initiative.value;
			return unit1_initiative.CompareTo(unit2_initiative);
		});
		return new Queue<Unit>(turnarray);

    }

}
