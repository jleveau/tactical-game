using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileMenu : MonoBehaviour {
    
	Profile profile;
	public GameObject profile_menu_item_prefab;
	public GameObject content;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setProfile(Profile profile) {
		this.profile = profile;
		foreach (Statistic stat in profile.getAllStatistics()) {
			GameObject item = Instantiate(profile_menu_item_prefab);
            item.transform.SetParent(content.transform, false);

			ProfileMenuItem item_script = item.GetComponent<ProfileMenuItem>();
            item_script.setStatistic(stat);
         
            item.SetActive(true);
		}
	}
}
