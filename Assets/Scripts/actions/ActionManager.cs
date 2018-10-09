using System;
using System.Collections.Generic;
using UnityEngine;


public class ActionManager : MonoBehaviour
{
	List<GameObject> availableActionsTypes;
	public GameController gameController;
	public GameObject moveAction;
	public GameObject passAction;

	void Start()
    {
		availableActionsTypes = new List<GameObject>();
		availableActionsTypes.Add(moveAction);
		availableActionsTypes.Add(passAction);
	}
       
	public List<Action> getAvailableActionsForTarget(Unit unit, Vector3Int position) {
		List<Action> actions = new List<Action>();
		foreach (GameObject action_type in availableActionsTypes) {
			Action action = createAction(action_type, unit, position);
			if (action.condition()) {
				actions.Add(action);
			}
		}
		return actions;
	}

	Action createAction(GameObject action_prefab, Unit unit, Vector3Int position) {
		GameObject action_obj = Instantiate(action_prefab);
        Action action = action_obj.GetComponent<Action>();

        action.performer = unit;
		action.target = position;
        action.controller = gameController;
		action.transform.SetParent(this.transform);
		return action;
	}
    
}
