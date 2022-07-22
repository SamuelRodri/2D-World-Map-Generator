using Generator.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.Polygons
{
    public class MultiPolygon : IPolygon<Vector3>
    {
        public List<IPolygon<Vector3>> polygons = new List<IPolygon<Vector3>>();

        public MultiPolygon(List<List<List<List<float>>>> Mcoordinates)
        {
            type = PolygonType.MultiPolygon;

            for(int i = 0; i < Mcoordinates.Count; i++)
            {
                // It's a simple polygon
                if (Mcoordinates[i].Count == 1)
                {
                    // It doesn't have holes
                    polygons.Add(new SimplePolygon(Mcoordinates[i]));
                }
                else
                {
                    // It has holes
                    polygons.Add(new SimplePolygonWithHoles(Mcoordinates[i]));
                }
            }
        }

        public override Vector3[] GetVertices()
        {
            throw new System.NotImplementedException();
        }
    }
}
