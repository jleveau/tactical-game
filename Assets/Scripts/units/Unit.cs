using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Unit : MonoBehaviour
{
	[NonSerialized]
	public Vector3Int tile_position;
	public string profile_json;

	public Profile profile;

	void Start()
	{
		loadProfileFromJSON();
	}
	
	// Update is called once per frame
	void Update()
	{
	}
   
	public void new_turn_update() {
		profile.UpdateTurnChange();
	}

	public void inflictDamage(int damages, Unit target) {
		target.receiveDamage(damages);
	}

	public void receiveDamage(int damage) {
		profile.health_points.value -= damage;
	}

	void loadProfileFromJSON()
    {
		string filePath = Path.Combine(Application.streamingAssetsPath, profile_json);
        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
			Profile loadedProfile = JsonUtility.FromJson<Profile>(dataAsJSON);
			profile = loadedProfile;
        }
        else
        {
            Debug.LogError("Cannot load profile for path " + filePath);
        }
    }

	void saveProfileToJson(string filename) {
		string filePath = Path.Combine(Application.streamingAssetsPath, filename);
		string json_string = JsonUtility.ToJson(profile);
		File.WriteAllText(filePath, json_string);
	}
    
}
