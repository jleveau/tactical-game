using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionMenuScript : MonoBehaviour {

	public GameObject actionItemPrefab;
	public GameObject content;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setActions(List<Action> actions) {
		foreach (Transform child in content.transform)
        {
			Debug.Log(child);
			Destroy(child.gameObject);
        }
		foreach (Action action in actions) {
			GameObject item = Instantiate(actionItemPrefab);
            item.SetActive(true);
            item.GetComponent<actionMenuItemScript>().setAction(action);
			item.transform.SetParent(content.transform, false);
		}

	}


}
