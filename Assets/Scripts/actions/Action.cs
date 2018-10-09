
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {

    
	List<IActionObserver> observers;
	public Unit performer;
	public GameController controller;
	public Vector3Int target;
    
	void Start()
    {
		observers = new List<IActionObserver>();
	}
    
	//Condition to perform the action
	public abstract bool condition();

    //What does the action do
	public abstract void perform();

	protected void NotifyActionStarted() {
		foreach(IActionObserver observer in observers) {            
			observer.NotifyActionStarted(performer, this);
		}
	}

	protected void NotifyActionFinished() {
		foreach (IActionObserver observer in observers)
        {
			observer.NotifyActionFinished(performer, this);
        }	
	}

	public void addObserver(IActionObserver observer) {

		if (observers == null) {
			observers = new List<IActionObserver>();
		}
		observers.Add(observer);
	}


	public abstract string getActionText();
}
