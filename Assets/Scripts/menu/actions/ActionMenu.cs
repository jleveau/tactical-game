using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{


    public GameObject actionItemPrefab;
    public GameObject content;
    public MenuController menu_controller;
	public List<KeyCode> input_list;

    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setActions(List<Action> actions)
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        int i = 0;

        foreach (Action action in actions)
        {
            GameObject item = Instantiate(actionItemPrefab);
            item.transform.SetParent(content.transform, false);

            actionMenuItem item_script = item.GetComponent<actionMenuItem>();
            item_script.setAction(action);
			if (i < input_list.Count) {
			    item_script.setTrigger(input_list[i]);
			}

            item_script.setController(menu_controller);
            item.SetActive(true);
            ++i;
        }

    }

}
