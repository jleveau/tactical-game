using System;
using System.Collections.Generic;

public interface IStatistic
{
	int Value { get; }
	void UpdateTurnChange();
}

[Serializable]
public class Statistic
{
	protected int current_value;

	public int Value
	{ 
		get { return current_value;  }  
	}
  
	public virtual void UpdateTurnChange() {
	}
}

public class ResetEachTurnStatistic : Statistic
{

	public int reset_value;

	ResetEachTurnStatistic() {
		reset_value = current_value;
	}

	public override void UpdateTurnChange()
    {
		base.UpdateTurnChange();
		current_value = reset_value;
    }
}

[Serializable]
public class Profile
{
	public Statistic initiative;
	public ResetEachTurnStatistic movement_points;
	public Statistic health_points;
	public Statistic attack;

	public List<ProfileObserver> observers;


	public void UpdateTurnChange() {
		
	}

}



