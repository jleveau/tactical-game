using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.TileMapElements.Units
{
	[Serializable]
    public class Statistic
	{
		public int reset_value;
		public int current_value;
	}

	[Serializable]
	public class Profile
    {
		public Statistic initiative;
		public Statistic movement_points;

		public void updateStatistics() {
			initiative.current_value = initiative.reset_value;
			movement_points.current_value = movement_points.reset_value;
		}
    }


}
