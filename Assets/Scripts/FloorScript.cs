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

    }

	private void OnMouseOver()
    {
		mapManager.onOverFloor(Input.mousePosition);
    }

}
