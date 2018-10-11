using System;
using System.Collections.Generic;

[Serializable]
public class Profile {

	public Statistic initiative = new Statistic("Initiative", 0);
	public Statistic health_points = new Statistic("Health Points", 0);
	public Statistic damages = new Statistic("Damages", 0);
	public ResetEachTurnStatistic movement_points = new ResetEachTurnStatistic("Movement Points", 0, 0);
	public ResetEachTurnStatistic action_points = new ResetEachTurnStatistic("Action Points", 0, 0);

	List<Statistic> statistics;

	Profile() {
		statistics = new List<Statistic>();

		initiative = new Statistic("Initiative", 0);
        health_points = new Statistic("Health Points", 0);
        damages = new Statistic("Damages", 0);
        movement_points = new ResetEachTurnStatistic("Movement Points", 0, 0);
        action_points = new ResetEachTurnStatistic("Action Points", 0, 0);

		statistics.Add(initiative);
		statistics.Add(health_points);
		statistics.Add(damages);
		statistics.Add(movement_points);
		statistics.Add(action_points);
	}

	public void UpdateTurnChange() {
		
		foreach (Statistic stat in statistics) {
			stat.UpdateTurnChange();
		}
	}
    
	public List<Statistic> getAllStatistics() {
		return statistics;
	}


}

