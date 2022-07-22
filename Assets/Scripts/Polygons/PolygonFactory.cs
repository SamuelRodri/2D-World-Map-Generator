using Generator.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.Polygons
{
    public static class PolygonFactory
    {
        public static IPolygon<Vector3> CreatePolygon(Geometry geometry)
        {
            if (geometry.type.Equals("Polygon"))
            {
                // It's a simple polygon
                if(geometry.coordinates.Count == 1)
                {
                    // It doesn't have holes
                    return new SimplePolygon(geometry.coordinates);
                }
                else
                {
                    // It has holes
                    return new SimplePolygonWithHoles(geometry.coordinates);
                }
            }else if (geometry.type.Equals("MultiPolygon"))
            {
                // It's a multipolygon
                return new MultiPolygon(geometry.Mcoordinates);
            }
            else
            {
                return null;
            }
        }
    }
}
