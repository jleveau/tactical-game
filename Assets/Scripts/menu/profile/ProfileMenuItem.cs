
using UnityEngine;
using UnityEngine.UI;

public class ProfileMenuItem : MonoBehaviour {

	Statistic statistic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
  		GetComponent<Text>().text = statistic.name + " : " + statistic.value;
	}

    public void setStatistic(Statistic stat) {
		statistic = stat;
	}
}
