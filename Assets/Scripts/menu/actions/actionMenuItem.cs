using UnityEngine;
using UnityEngine.UI;

public class actionMenuItem : MonoBehaviour {


	Action action;
	MenuController controller;
	KeyCode trigger;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(trigger)) {
			Debug.Log("in");
			this.controller.selectionAction(action);
		}
	}

	public void setAction(Action action) {
		this.action = action;
		updateText();
	}

	public void setController(MenuController menuController) {
		this.controller = menuController;
	}

	public void setTrigger(KeyCode code) {
		trigger = code;
		updateText();
	}

	private void updateText() {
		string s = "";
		if (trigger != KeyCode.None) {
			s += trigger + " ";
		}
		s += action.getActionText();
		GetComponent<Text>().text = s;
	}

}
