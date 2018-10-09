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
	public int value;
  
	public virtual void UpdateTurnChange() {
	}
}
[Serializable]
public class ResetEachTurnStatistic : Statistic
{
	public int reset_value;
       
	public override void UpdateTurnChange()
    {
		base.UpdateTurnChange();
		value = reset_value;
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
		initiative.UpdateTurnChange();
		movement_points.UpdateTurnChange();
		health_points.UpdateTurnChange();
		attack.UpdateTurnChange();
	}

}



