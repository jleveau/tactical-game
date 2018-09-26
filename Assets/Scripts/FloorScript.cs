using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

	public MapManager mapManager;

	// Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			mapManager.selectCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

	private void OnMouseOver()
    {
		mapManager.onOverFloor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
