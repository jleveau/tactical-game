using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;

public class UnitManager {
    
	private List<Unit> units;
	private Queue<Unit> turnQueue;

	public Unit currentUnit;


    public UnitManager() {
		units = new List<Unit>();
		turnQueue = new Queue<Unit>();
	}

	public void addUnit(Unit unit) {
		units.Add(unit);
	}
   
    
	public void nextUnit() {
		
		if (turnQueue.Count == 0)
        {
            turnQueue = buildTurnQueue();
        }

		if (turnQueue.Count > 0) {
			currentUnit = turnQueue.Dequeue();
			currentUnit.new_turn_update();
		}

    }

	private Queue<Unit> buildTurnQueue()
    {
		Unit[] turnarray = units.ToArray();
		Array.Sort(turnarray, delegate (Unit obj1, Unit obj2)
		{
			Unit unit1 = obj1.GetComponent<Unit>();
			Unit unit2 = obj2.GetComponent<Unit>();
			return unit1.profile.initiative.current_value.CompareTo(unit2.profile.initiative.current_value);
		});
		return new Queue<Unit>(turnarray);

    }
    

}
