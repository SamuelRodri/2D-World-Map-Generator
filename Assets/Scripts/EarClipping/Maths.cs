using Generator.DataStructures;
using Generator.Polygons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Generator.EarClipping
{
	public static class Maths
	{
		// Check if a Point is an ear
		public static bool isEar(Point<Vector3> prev, Point<Vector3> Point, Point<Vector3> next, LinkedList<Point<Vector3>> nodes)
		{
			Vector3 D = prev.value - Point.value;
			Vector3 E = next.value - Point.value;

			for (int i = 0; i < nodes.Count; i++)
			{
				var index = nodes.ElementAt(i).index;
				var point = nodes.ElementAt(i).value;

				if (index != prev.index && index != Point.index && index != next.index)
				{
					if (!point.Equals(prev.value) && !point.Equals(Point.value) && !point.Equals(next.value))
					{
						Vector3 A = Point.value;
						Vector3 B = prev.value;
						Vector3 C = next.value;
						Vector3 P = nodes.ElementAt(i).value;

						if (Mathf.Approximately(A.y, C.y)) { C.y += 0.0001f; }
						if (Mathf.Approximately(0, E.y)) { E.y += 0.0001f; }
						float w1 = (E.x * (A.y - P.y) + E.y * (P.x - A.x)) / ((D.x * E.y) - (D.y * E.x));
						float w2 = (P.y - A.y - w1 * D.y) / E.y;

						if (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Check if a Point is convex
		public static bool isConvex(Vector3 prev, Vector3 Point, Vector3 next)
		{

			Vector3 BA = prev - Point;
			Vector3 BC = next - Point;

			var angle = Mathf.Acos(Vector3.Dot(BA, BC) / (Vector3.Magnitude(BA) * Vector3.Magnitude(BC)));

			var det = ((Point.x - prev.x) * (next.y - Point.y) - (next.x - Point.x) * (Point.y - prev.y));

			if (det < 0)
			{
				return angle < Mathf.PI;
			}
			else
			{
				return ((2 * Mathf.PI) - angle) < Mathf.PI;
			}
		}

		// Check if a Point is reflex
		public static bool isReflex(Vector3 prev, Vector3 Point, Vector3 next)
		{
			Vector3 BA = prev - Point;
			Vector3 BC = next - Point;

			var angle = Mathf.Acos(Vector3.Dot(BA, BC) / (Vector3.Magnitude(BA) * Vector3.Magnitude(BC)));

			var det = ((Point.x - prev.x) * (next.y - Point.y) - (next.x - Point.x) * (Point.y - prev.y));

			if (det > 0)
			{
				return angle > Mathf.PI;
			}
			else
			{
				return ((2 * Mathf.PI) - angle) > Mathf.PI;
			}
		}

		public static Vector2[] GetMutallyVisibleVertices(LinkedList<Point<Vector3>> outerPolygon, LinkedList<Point<Vector3>> innerPolygon)
		{
			Vector2 M = new Vector2(0, 0); ;
			for (int i = 0; i < innerPolygon.Count; i++)
			{
				for (int j = 0; j < innerPolygon.Count; j++)
				{
					if (i != j && innerPolygon.ElementAt(i).value.Equals(innerPolygon.ElementAt(j).value))
					{
						Debug.Log(i);
						Debug.Log(j);
					}
				}
			}

			// Finding the vertex with the maximum x-value
			for (int i = 0; i < innerPolygon.Count; i++)
			{
				if (innerPolygon.ElementAt(i).value.x > M.x)
				{
					M = new Vector2(innerPolygon.ElementAt(i).value.x, innerPolygon.ElementAt(i).value.y);
				}
			}
			// Ecuación Vectorial de la recta M => (x, y) = (M.x, M.y) + t(1, 0)

			// Finding an intersect between the ray M + t(1,0) and one edge of the outer polygon
			for (int i = 0; i < outerPolygon.Count; i++)
			{
				Vector2 A = new Vector2(outerPolygon.ElementAt(i).value.x, outerPolygon.ElementAt(i).value.y);
				Point<Vector3> value = outerPolygon.ElementAt((Mathf.Abs((i + 1) * outerPolygon.Count) + (i + 1)) % outerPolygon.Count);
				Vector2 B = new Vector2(value.value.x, value.value.y);

				Vector2 v = B - A; // Vector de la recta

				// Ecuación vectorial de la recta => (x, y) = (AA.x, AA.y) + k(v.x, v.y)
				if (v.y != 0) // Intersectan
				{
					float k = (M.y - A.y) / (v.y);
					float t = (M.x - A.x - (k * v.x)) / -1;

					if (t >= 0 && k >= 0) // Intersectan por la derecha
					{
						Vector2 I = new Vector2(M.x + t, M.y);

						if (I.x > A.x && I.x > B.x)
						{
							continue;
						}
						for (int j = 0; j < outerPolygon.Count; j++)
						{
							Vector2 point = outerPolygon.ElementAt(j).value;
							if (point.Equals(I)) // I is a outerPolygon vertex
							{
								Debug.Log("M & I are mutually visibles");
								return new Vector2[]
								{
								M, I
								};
							}
						}

						// I is not an outerPolygon vertex
						Vector2 P = A.x > B.x ? A : B;          // P gets the maximum x-value of the edge AB
						Vector2 R = new Vector2();
						bool isInside = false;

						for (int j = 0; j < outerPolygon.Count; j++) // Search for reflexives vertices
						{
							Point<Vector3> prev = outerPolygon.ElementAt((Mathf.Abs((j - 1) * outerPolygon.Count) + (j - 1)) % outerPolygon.Count);
							Point<Vector3> node = outerPolygon.ElementAt((Mathf.Abs(j * outerPolygon.Count) + j) % outerPolygon.Count);
							Point<Vector3> next = outerPolygon.ElementAt((Mathf.Abs((j + 1) * outerPolygon.Count) + (j + 1)) % outerPolygon.Count);

							if (isReflex(prev.value, node.value, next.value) && node.value.x != P.x && node.value.y != P.y)
							{
								if (isInsideTriangle(prev.value, node.value, next.value, P))
								{
									isInside = true;
									break;
								}

								if (Math.Sqrt(Math.Pow(M.x - node.value.x, 2) + Math.Pow(M.y - node.value.y, 2)) > Math.Sqrt(Math.Pow(M.x - R.x, 2) + Math.Pow(M.y - R.y, 2)))
								{
									R = node.value; // node is the nearest reflex vertices to M
								}

							}
						}

						if (!isInside)
						{
							Debug.Log("M & P are mutually visibles");
							return new Vector2[]
							{
							M, P
							};
						}
						else
						{
							Debug.Log("M & R are mutually visibles");
							return new Vector2[]
							{
							M, R
							};
						}
					}
				}
			}
			return null;
		}

		// Check if P is inside the ABC triangle
		private static bool isInsideTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
		{
			Vector3 D = A - B;
			Vector3 E = C - B;

			if (Mathf.Approximately(A.y, C.y)) { C.y += 0.0001f; }

			float w1 = (E.x * (A.y - P.y) + E.y * (P.x - A.x)) / ((D.x * E.y) - (D.y * E.x));
			float w2 = (P.y - A.y - w1 * D.y) / E.y;

			return (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1);
		}
	}
}
