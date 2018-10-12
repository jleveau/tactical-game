using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.algorithm
{
    public class Node
	{
		public int x;
		public int y;
		public int cost;
		public int transition_cost;

		public Node(int x, int y, int cost, int transition_cost)
		{
			this.x = x;
			this.y = y;
			this.cost = cost;
			this.transition_cost = transition_cost;
		}
	}

    public class GraphPathSolver
    {
		Dictionary<Vector2Int, int> cost_per_tile;
		List<Node> nodes;

		void loadGraph(int[][] costs) {
			nodes = new List<Node>();
			for (int i = 0; i < costs.Length; ++i) {
				for (int j = 0; j < costs[i].Length; ++j) {
					Node node = new Node(i, j, -1, costs[i][j]);
				}
			}
		}

    }
}
