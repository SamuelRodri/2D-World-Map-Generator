using Generator.DataStructures;
using Generator.Polygons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Generator.EarClipping
{
	/*
	 * Class with all the necessary methods to perform the triangulation
	 */
	public class Triangulator
	{
		// Implements the EarClipping Algorithm and return a array with the indexes of triangles
		public static int[] EarClipping(IPolygon<Vector3> polygon)
		{
			List<List<int>> data = CheckPolygon(polygon);
			List<int> tris = new List<int>();
			List<int> E = data[0];
			List<int> C = data[1];
			List<int> R = data[2];

			while (polygon.nodes.Count > 3)
			{
				int ear = E[0];
				LinkedListNode<Point<Vector3>> node = polygon.nodes.First;
				for (int i = 0; i < polygon.nodes.Count; i++)
				{
					if (node.Value.index == ear)
					{
						break;
					}
					else
					{
						node = node.Next;
					}
				}
				if (node.Previous == null)
				{
					tris.Add(polygon.nodes.Last.Value.index);
					tris.Add(node.Value.index);
					tris.Add(node.Next.Value.index);
				}
				else if (node.Next == null)
				{
					tris.Add(node.Previous.Value.index);
					tris.Add(node.Value.index);
					tris.Add(polygon.nodes.First.Value.index);
				}
				else
				{
					tris.Add(node.Previous.Value.index);
					tris.Add(node.Value.index);
					tris.Add(node.Next.Value.index);
				}

				polygon.nodes.Remove(node);
				data = CheckPolygon(polygon);

				E = data[0];
				C = data[1];
				R = data[2];
			}

			tris.Add(polygon.nodes.First.Value.index);
			tris.Add(polygon.nodes.First.Next.Value.index);
			tris.Add(polygon.nodes.First.Next.Next.Value.index);

			return tris.ToArray();
		}

		/*
		 * Checks the vertices of a polygon and establishes the E,C,R lists
		 * 
		 * - E -> Ears
		 * - C -> Convex vertices
		 * - R -> Reflex vertices
		 */
		public static List<List<int>> CheckPolygon(IPolygon<Vector3> polygon)
		{
			List<List<int>> data = new List<List<int>>();
			List<int> E = new List<int>();
			List<int> C = new List<int>();
			List<int> R = new List<int>();

			for (int i = 0; i < polygon.nodes.Count; i++)
			{
				Point<Vector3> prev = polygon.nodes.ElementAt((Mathf.Abs((i - 1) * polygon.nodes.Count) + (i - 1)) % polygon.nodes.Count);
				Point<Vector3> node = polygon.nodes.ElementAt((Mathf.Abs(i * polygon.nodes.Count) + i) % polygon.nodes.Count);
				Point<Vector3> next = polygon.nodes.ElementAt((Mathf.Abs((i + 1) * polygon.nodes.Count) + (i + 1)) % polygon.nodes.Count);

				if (Maths.isConvex(prev.value, node.value, next.value))
				{
					C.Add(node.index);

					if (Maths.isEar(prev, node, next, polygon.nodes))
					{
						E.Add(node.index);
						continue;
					}
				}

				else
				{
					R.Add(node.index);
				}
			}

			data.Add(E);
			data.Add(C);
			data.Add(R);

			return data;
		}
	}	
}
