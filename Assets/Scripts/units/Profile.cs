using System;
using System.Collections.Generic;
using System.Linq;

public interface IStatistic
{
	int Value { get; }
	void UpdateTurnChange();
}

[Serializable]
public class Statistic
{
	public int value;
	public string name;

	public Statistic(string name, int value) {
		this.name = name;
		this.value = value;
	}
  
	public virtual void UpdateTurnChange() {
	}
}
[Serializable]
public class ResetEachTurnStatistic : Statistic
{
	public int reset_value;

    public ResetEachTurnStatistic(string name, int value, int reset_value) : base(name, value)
	{
		this.reset_value = reset_value;
	}

       
	public override void UpdateTurnChange()
    {
		base.UpdateTurnChange();
		value = reset_value;
    }
}

public enum StatisticEnum
{
    Initiative,
    Movement_Points,
    Action_Points,
    HealthPoints,
    Damages
}

[Serializable]
public class Profile
{
	[NonSerialized]
	Dictionary<StatisticEnum, Statistic> statistics_dict;

	[NonSerialized]
	public List<ProfileObserver> observers;

	public Profile() {
		statistics_dict = new Dictionary<StatisticEnum, Statistic>();

		statistics_dict.Add(StatisticEnum.Initiative, new Statistic("Initiative", 0));
		statistics_dict.Add(StatisticEnum.HealthPoints, new Statistic("Health Points", 0));
        statistics_dict.Add(StatisticEnum.Damages, new Statistic("Damages", 0));

		statistics_dict.Add(StatisticEnum.Movement_Points, new ResetEachTurnStatistic("Movement Points", 0, 0));
		statistics_dict.Add(StatisticEnum.Action_Points, new ResetEachTurnStatistic("Action Points", 0, 0));
	}

	public void UpdateTurnChange() {
		foreach (Statistic stat in statistics_dict.Values) {
			stat.UpdateTurnChange();
		}
	}

	public Statistic getStatistic(StatisticEnum statistic) {
		return statistics_dict[statistic];
	}
    
	public List<Statistic> getAllStatistics() {
		return statistics_dict.Values.ToList();
	}
}

