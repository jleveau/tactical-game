using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {


	public GameObject actionMenu;
    public GameObject GUICanvas;

	// Use this for initialization
	void Start () {
		actionMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void displayActionMenu(Vector3 pos, List<Action> actions)
    {
		actionMenu.SetActive(true);
		actionMenu.transform.position = pos;
		actionMenu.GetComponent<actionMenuScript>().setActions(actions);
    }

}
