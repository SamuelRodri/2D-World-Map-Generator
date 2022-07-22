using Generator.DataStructures;
using Generator.EarClipping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Generator.Polygons
{
    public class SimplePolygonWithHoles : IPolygon<Vector3>
    {
        LinkedList<Point<Vector3>> outerPolygon = new LinkedList<Point<Vector3>>();
        List<LinkedList<Point<Vector3>>> innersPolygons = new List<LinkedList<Point<Vector3>>>();

        public SimplePolygonWithHoles(List<List<List<float>>> coordinates)
        {
            type = PolygonType.SimplePolygonWithHoles;

            // Generating outer polygon
            for(int i = 0; i < coordinates[0].Count; i++)
            {
                outerPolygon.AddLast(new Point<Vector3>(i, new Vector3(coordinates[0][i][0], coordinates[0][i][1])));
                nodes.AddLast(new Point<Vector3>(i, new Vector3(coordinates[0][i][0], coordinates[0][i][1])));
            }

            // Generating inner polygons
            LinkedList<Point<Vector3>> innerPolygon = new LinkedList<Point<Vector3>>();
            
            for (int i = 1; i <= coordinates.Count - 1; i++)
            {
                innerPolygon = new LinkedList<Point<Vector3>>();

                for (int j = 0; j < coordinates[i].Count; j++)
                {
                    innerPolygon.AddLast(new Point<Vector3>(i, new Vector3(coordinates[i][j][0], coordinates[i][j][1])));
                    nodes.AddLast(new Point<Vector3>(i, new Vector3(coordinates[i][j][0], coordinates[i][j][1])));
                }
                innersPolygons.Add(innerPolygon);
            }

            outerPolygon.RemoveLast();
        }     
        
        public override Vector3[] GetVertices()
        {
            Vector2[] mutuallyVisibleVertices = Maths.GetMutallyVisibleVertices(outerPolygon, innersPolygons[0]);

            LinkedListNode<Point<Vector3>> point = outerPolygon.First;

            Vector3 M = mutuallyVisibleVertices[0];
            Vector3 H = mutuallyVisibleVertices[1];

            Point<Vector3> outerDuplicated = new Point<Vector3>(outerPolygon.Count + innersPolygons[0].Count, new Vector3(H.x, H.y));
            Point<Vector3> innerDuplicated = new Point<Vector3>(outerPolygon.Count + innersPolygons.Count + 1, new Vector3(M.x, M.y));

            LinkedList<Point<Vector3>> finalList = new LinkedList<Point<Vector3>>();
            bool found = false;

            while (point != null)
            {
                if (point.Value.value.Equals(H))
                {
                    finalList.AddLast(new Point<Vector3>(point.Value.index, point.Value.value));
                    LinkedList<Point<Vector3>> beforeFind = new LinkedList<Point<Vector3>>();
                    LinkedList<Point<Vector3>> afterFind = new LinkedList<Point<Vector3>>();
                    LinkedListNode<Point<Vector3>> innerNode = innersPolygons[0].First;

                    while (innerNode != null)
                    {
                        if (found)
                        {
                            afterFind.AddLast(new Point<Vector3>(innerNode.Value.index, innerNode.Value.value));
                        }
                        else
                        {
                            if (innerNode.Value.value.Equals(M))
                            {
                                found = true;
                                afterFind.AddFirst(new Point<Vector3>(innerNode.Value.index, innerNode.Value.value));
                            }
                            else
                            {
                                beforeFind.AddLast(new Point<Vector3>(innerNode.Value.index, innerNode.Value.value));
                            }
                        }
                        innerNode = innerNode.Next;
                    }

                    afterFind = new LinkedList<Point<Vector3>>(afterFind.Concat(beforeFind));
                    afterFind.AddLast(innerDuplicated);
                    afterFind.AddLast(outerDuplicated);

                    LinkedListNode<Point<Vector3>> point2 = afterFind.First;

                    while (point2 != null)
                    {
                        finalList.AddLast(new Point<Vector3>(point2.Value.index, point.Value.value));
                        point2 = point2.Next;
                    }
                }
                else
                {
                    finalList.AddLast(new Point<Vector3>(point.Value.index, point.Value.value));
                }
                point = point.Next;
            }

            Vector3[] vertices = new Vector3[finalList.Count];

            for (int i = 0; i < finalList.Count; i++)
            {
                vertices[i] = finalList.ElementAt(i).value;
            }

            return vertices;
        }
    }
}
