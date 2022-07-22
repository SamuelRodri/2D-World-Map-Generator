using Generator.DataStructures;
using Generator.EarClipping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Generator.Polygons
{
    public class SimplePolygon : IPolygon<Vector3>
    {
        public SimplePolygon(List<List<List<float>>> coordinates)
        {
            type = PolygonType.SimplePolygon;

            for (int i = 0; i < coordinates.Count; i++) // Coordinates only has one item
            {
                for (int j = 0; j < coordinates[i].Count - 1; j++)
                {
                    float x = coordinates[i][j][0];
                    float y = coordinates[i][j][1];

                    AddPoint(new Point<Vector3>(i + j, new Vector3(x, y)));
                }
            }
        }

        #region Methods
        private void AddPoint(Point<Vector3> node)
        {
            if(nodes.Count == 0)
            {
                nodes.AddFirst(node);
            }
            else
            {
                nodes.AddLast(node);
            }
        }

        public void DeletePoint(Point<Vector3> node)
        {
            nodes.Remove(node);
        }

        public override Vector3[] GetVertices()
        {
            Vector3[] vertices = new Vector3[nodes.Count];

            LinkedListNode<Point<Vector3>> node = nodes.First;

            for (int i = 0; i < nodes.Count; i++)
            {
                vertices[i] = node.Value.value;
                node = node.Next;
            }

            return vertices;
        }
    }
    #endregion
}

