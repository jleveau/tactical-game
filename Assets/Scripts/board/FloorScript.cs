using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

	public BoardController controller;

	// Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			controller.onFloorClicked(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

	private void OnMouseOver()
    {
		controller.onOverFloor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
