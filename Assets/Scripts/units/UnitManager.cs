using System;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Scripts.TileMapElements;
using UnityEngine;

public class UnitManager {
    
	private List<GameObject> units;
	private Queue<GameObject> turnQueue;

    public UnitManager() {
		units = new List<GameObject>();
        turnQueue = new Queue<GameObject>();
	}

	public void addUnit(GameObject unit) {
		units.Add(unit);
	}
    
	public GameObject getNextUnit() {
		
		if (turnQueue.Count == 0)
        {
            turnQueue = buildTurnQueue();
        }
		if (turnQueue.Count > 0) {
        	return turnQueue.Dequeue();
		} else {
			return null;
		}
    }

    private Queue<GameObject> buildTurnQueue()
    {
		GameObject[] turnarray = units.ToArray();
		Array.Sort(turnarray, delegate (GameObject obj1, GameObject obj2)
		{
			Unit unit1 = obj1.GetComponent<Unit>();
			Unit unit2 = obj2.GetComponent<Unit>();
			return unit1.profile.initiative.CompareTo(unit2.profile.initiative);
		});
		return new Queue<GameObject>(turnarray);

    }
    

}
