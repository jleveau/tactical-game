using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

	public Board board;

	// Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			board.onFloorClicked(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

	private void OnMouseOver()
    {
		board.onOverFloor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
