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
		void Init(int[][] costs) {
			nodes = new List<Node>();
			for (int i = 0; i < costs.Length; ++i) {
				for (int j = 0; j < costs[i].Length; ++j) {
					Node node = new Node(i, j, -1, costs[i][j]);
				}
			}
			foreach(Node node in this.nodes) {
				node.neighbors = getNeighbors(node);
			}
		}

		List<Node> getNeighbors(Node node) {
			List<Node> neighbors = new List<Node>(); 
			foreach(Node neighbor in this.nodes) {
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
		
		Node min_node(List<Node> nodes) {
			if (nodes.Count == 0) {
				throw new Exception("nodes is empty");
			}
			Node min_node = nodes[0];
			foreach(Node node in nodes) {
				if (node.cost < min_node.cost) {
					min_node = node;
				}
			}
			return min_node;
		}

		void maj_distance(Node n1, Node n2) {
			if (n2.cost == -1) {
				n2.cost = n1.cost + n2.transition_cost;
			}
			else if (n2.cost > n1.cost + n2.transition_cost) {
				n2.cost = n1.cost + n2.transition_cost;
				n2.parent = n1;
			}
		}

		public void build(int[][] costs) {
			Init(costs);
			List<Node> node_pool = new List<Node>(this.nodes);

			while (node_pool.Count > 0) {
				Node n1 = min_node(node_pool);
				node_pool.Remove(n1);
				foreach(Node n2 in n1.neighbors) {
					maj_distance(n1, n2);
				}
			}
		}

		public LinkedList<Vector2Int> getPathForPosition(Vector2Int pos) {
			Node n;
			LinkedList<Vector2Int> path = new LinkedList<Vector2Int>();
			foreach(Node node in this.nodes) {
				if(node.x == pos.x && node.y == pos.y) {
					n = node;
					while (n.parent != null) {
						path.AddFirst(new Vector2Int(n.x, n.y));
						n = n.parent;
					}
					return path;
				}
			}
			throw new Exception("cannot find node for pos");
		}

    }
}
