using UnityEngine;

public class ProfileMenu : MonoBehaviour {
    
	public GameObject profile_menu_item_prefab;
	public GameObject content;
	public MenuController menu_controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setProfile(Profile profile) {
		foreach (Transform child in content.transform)
		{
			Destroy(child.gameObject);
		}
		foreach (Statistic stat in profile.getAllStatistics()) {
			GameObject item = Instantiate(profile_menu_item_prefab);
            item.transform.SetParent(content.transform, false);

			ProfileMenuItem item_script = item.GetComponent<ProfileMenuItem>();
            item_script.setStatistic(stat);
         
            item.SetActive(true);
		}
	}
}
