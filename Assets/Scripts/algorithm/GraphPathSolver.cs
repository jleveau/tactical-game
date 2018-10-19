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
		public Node parent;
		public List<Node> neighbors;

		public Node(int x, int y, int cost, int transition_cost)
		{
			this.x = x;
			this.y = y;
			this.cost = cost;
			this.transition_cost = transition_cost;
			this.parent = null;
			this.neighbors = new List<Node>();
		}
	}

    public class GraphPathSolver
    {
		Dictionary<Vector2Int, int> cost_per_tile;
		List<Node> nodes;


		// costs[i][j] = -1 => cout infini
		void Init(int[][] costs, Vector2Int start) {
			nodes = new List<Node>();
			Debug.Log(start.x + " " + start.y);
			for (int i = 0; i < costs.Length; ++i) {
				for (int j = 0; j < costs[i].Length; ++j) {
					Node node;
					if (start.x == i && start.y == j) {
						node = new Node(i, j, 0, costs[i][j]);
					} else {
						node = new Node(i, j, 100000, costs[i][j]);
					}
					nodes.Add(node);
				}
			}
			foreach(Node node in nodes) {
				node.neighbors = getNeighbors(node);
			}
		}

		List<Node> getNeighbors(Node node) {
			List<Node> neighbors = new List<Node>(); 
			foreach(Node neighbor in nodes) {
				if (neighbor.x == node.x-1 && neighbor.y == node.y ||
					neighbor.x == node.x+1 && neighbor.y == node.y ||
					neighbor.x == node.x && neighbor.y == node.y+1 ||
					neighbor.x == node.x && neighbor.y == node.y-1
				) {
					neighbors.Add(neighbor);
				}
			}
			return neighbors;
		}

		Node min_node(List<Node> node_list) {
			if (node_list.Count == 0) {
				throw new Exception("nodes is empty");
			}
			Node min = node_list[0];
			foreach(Node node in node_list) {
				if (node.cost < min.cost) {
					min = node;
				}
			}
			if (min.cost == -1) {
				throw new Exception("no reachable node");
			}
			return min;
		}

		void maj_distance(Node n1, Node n2) {
            if (n2.cost > n1.cost + n2.transition_cost) {
				n2.cost = n1.cost + n2.transition_cost;
				n2.parent = n1;
			}
		}

		public void buildPaths(int[][] costs, Vector2Int start) {
			Init(costs, start);
			List<Node> node_pool = new List<Node>(nodes);

			while (node_pool.Count > 0) {
				Node node = min_node(node_pool);
				node_pool.Remove(node);
				foreach(Node node2 in node.neighbors) {
					maj_distance(node, node2);
				}
			}
		}

		public LinkedList<Vector2Int> getPathForPosition(Vector2Int end) {
			LinkedList<Vector2Int> path = new LinkedList<Vector2Int>();
			if (this.nodes.Count == 0) {
				return path;
			}

			Node node = nodes[0];
			Boolean found = false;
			int i = 1;
			while (i < this.nodes.Count && !found)
            {
				node = nodes[i];
				if (node.x == end.x && node.y == end.y) {
					found = true;
				}
				++i;
            }
			if (!found) {
				throw new Exception("can't find selected position");
			}
			while (node.parent != null) {
				path.AddFirst(new Vector2Int(node.x, node.y));
				node = node.parent;
			}
			Debug.Log("path size" + path.Count);

			return path;

		}

		public List<Vector2Int> getReachablePos(int max_cost) {
			List<Vector2Int> reachable_pos = new List<Vector2Int>();
			if (this.nodes.Count == 0)
            {
				return reachable_pos;
            }
			foreach(Node node in this.nodes) {
				if (node.cost <= max_cost) {
					reachable_pos.Add(new Vector2Int(node.x, node.y));
				}
			}
			return reachable_pos;
		}

    }
}
