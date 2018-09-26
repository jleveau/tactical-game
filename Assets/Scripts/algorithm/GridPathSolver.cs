using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Globalization;
using System;

namespace AssemblyCSharp.Assets.Scripts.algorithm
{
	public class GridPathSolver 
    {

		class Node {
			public int heuristic, cost;
			public Vector2Int pos;
			public Node parent;
            public Node(int heuristic, int cost, Vector2Int pos, Node parent)
			{
				this.heuristic = heuristic;
				this.cost = cost;
				this.pos = pos;
				this.parent = parent;
			}
		}
        

		private Node popNode(List<Node> nodes)  {
			Node min = nodes[0];
			for (int i = 0; i < nodes.Count; ++i) {
				if (min.heuristic > nodes[i].heuristic)
					min = nodes[i];
			}
			nodes.Remove(min);
			return min;
		}

		private List<Node> getNeighbors(Node node)
		{
			List<Node> neighbors = new List<Node>();
			neighbors.Add(new Node(0, 0, new Vector2Int(node.pos.x - 1, node.pos.y), node));
			neighbors.Add(new Node(0, 0, new Vector2Int(node.pos.x + 1, node.pos.y), node));
			neighbors.Add(new Node(0, 0, new Vector2Int(node.pos.x, node.pos.y - 1), node));
			neighbors.Add(new Node(0, 0, new Vector2Int(node.pos.x, node.pos.y + 1), node));

			return neighbors;
		}

		private bool existsWithInferiorCost(Node node, IEnumerable<Node> list) {
           
			foreach (Node existing_node in list) {
				if (existing_node.pos.x == node.pos.x && existing_node.pos.y == node.pos.y && existing_node.cost <= node.cost) {
					return true;
				}
			}
			return false;
		}

		private int distance(Vector2Int n1, Vector2Int n2) {
			return Math.Abs(n1.x - n2.x) + Math.Abs(n1.y - n2.y);
		}

		private List<Vector2Int> buildPath(Node node) {
			List<Vector2Int> path = new List<Vector2Int>();
			path.Add(node.pos);
			while (node.parent != null) 
			{
				path.Add(node.parent.pos);
				node = node.parent;
			}
			return path;
		}
      
      
		public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end) {

			Queue<Node> closedList = new Queue<Node>();
   			List<Node> openedList = new List<Node>();

			Node depart = new Node(0, 0, new Vector2Int(start.x, start.y), null);
			openedList.Add(depart);
			TimeSpan start_time= DateTime.Now.TimeOfDay;
			while (openedList.Count > 0) {
				Node node = popNode(openedList);
				if (node.pos.x == end.x && node.pos.y == end.y) {
					TimeSpan end_time = DateTime.Now.TimeOfDay;
					return buildPath(node);
				}
				foreach (Node neighbor in getNeighbors(node)) {
					neighbor.cost = node.cost + 1;               
					if (!(existsWithInferiorCost(neighbor, openedList) || existsWithInferiorCost(neighbor, closedList))) {
						neighbor.heuristic = neighbor.cost + distance(neighbor.pos, end);
						openedList.Add(neighbor);
					}
				}
				closedList.Enqueue(node);
			}

			return new List<Vector2Int>();
		}
	}
}
