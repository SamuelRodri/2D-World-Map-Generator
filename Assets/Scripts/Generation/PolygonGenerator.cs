using Generator.Data;
using System.Collections;
using System.Collections.Generic;
using TriangleNet.Geometry;
using UnityEngine;

namespace Generator.Generators
{
    public static class PolygonGenerator
    {
        public static Polygon CreatePolygon(Geometry geometry)
        {
            var polygon = new Polygon();

            if (geometry.type.Equals("Polygon"))
            {
                // SimplePolygon
                if (geometry.coordinates.Count == 1)
                {
                    // Without holes
                    Vertex[] vertex = new Vertex[geometry.coordinates[0].Count];
                    for (int i = 0; i < geometry.coordinates[0].Count; i++)
                    {
                        float x = geometry.coordinates[0][i][0];
                        float y = geometry.coordinates[0][i][1];

                        vertex[i] = new Vertex(x, y, 1);
                    }
                    polygon.Add(new Contour(vertex, 1));
                }
                else
                {
                    // With holes
                    Vertex[] vertex = new Vertex[geometry.coordinates[0].Count];
                    for (int i = 0; i < vertex.Length; i++)
                    {
                        float x = geometry.coordinates[0][i][0];
                        float y = geometry.coordinates[0][i][1];

                        vertex[i] = new Vertex(x, y, 1);
                    }
                    polygon.Add(new Contour(vertex, 1));

                    Vector3[] vertices2 = new Vector3[]
                    {
                        new Vector3(1.5f, 1.7f),
                        new Vector3(1.5f, 1.0f),
                        new Vector3(1.0f, 2.0f),
                        new Vector3(1.0f, 2.0f)
                    };

                    Vertex[] vertex2 = new Vertex[geometry.coordinates[1].Count];
                    for (int i = 0; i < geometry.coordinates[1].Count; i++)
                    {
                        float x = geometry.coordinates[1][i][0];
                        float y = geometry.coordinates[1][i][1];

                        vertex2[i] = new Vertex(x, y, 2);
                    }
                    polygon.Add(new Contour(vertex2, 2), new Point(vertex2[1].x, vertex2[1].y));
                }
            }else if (geometry.type.Equals("MultiPolygon"))
            {
                // MultiPolygon
                foreach(var c in geometry.Mcoordinates)
                {
                    Vertex[] vertex = new Vertex[c[0].Count];
                    for(int i = 0; i < c[0].Count; i++)
                    {
                        float x = c[0][i][0];
                        float y = c[0][i][1];

                        vertex[i] = new Vertex(x, y, i+1);
                    }
                    polygon.Add(new Contour(vertex, 1));
                }
            }
            return polygon;
        }
    }
}