using UnityEngine;
using System;
using System.IO;

public class Unit : MonoBehaviour
{
	[NonSerialized]
	public Vector3Int tile_position;

	public Profile profile;
	public string profile_json;

    public Profile Profile
	{
		get
		{
			return profile;
		}
	}
   

	void Start()
	{
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

   
    
}
