using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {


	public GameObject actionMenu;
    public GameObject GUICanvas;
	public GameController gameController;
    
	// Use this for initialization
	void Start () {
		actionMenu.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
	}

	public void displayActionMenu(List<Action> actions)
    {
		actionMenu.SetActive(true);
		ActionMenuController menu = actionMenu.GetComponent<ActionMenuController>();
		menu.setActions(actions);
    }

	public void selectionAction(Action action) {
		gameController.selectAction(action);
	}

}
