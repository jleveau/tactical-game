using System;
using System.Collections.Generic;

[Serializable]
public class Profile {

	public Statistic initiative = new Statistic("Initiative", 0);
	public Statistic health_points = new Statistic("Health Points", 0);
	public Statistic damages = new Statistic("Damages", 0);
	public ResetEachTurnStatistic movement_points = new ResetEachTurnStatistic("Movement Points", 0, 0);
	public ResetEachTurnStatistic action_points = new ResetEachTurnStatistic("Action Points", 0, 0);

	public void UpdateTurnChange() {      
		initiative.UpdateTurnChange();
		health_points.UpdateTurnChange();
		damages.UpdateTurnChange();
		movement_points.UpdateTurnChange();
		action_points.UpdateTurnChange();
	}
    
	public IEnumerable<Statistic> getAllStatistics() {
		return new Statistic[] {initiative, health_points, damages, movement_points, action_points};
	}
    
	public void load(Profile profile) {
		this.initiative = profile.initiative;
		this.health_points = profile.health_points;
		this.damages = profile.damages;
		this.movement_points = profile.movement_points;
		this.action_points = profile.action_points;
	}
}

