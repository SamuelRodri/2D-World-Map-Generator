using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonFactory
{
    public static Polygon CreatePolygon(Geometry geometry)
    {
        if (geometry.type.Equals("Polygon"))
        {
            if(geometry.coordinates.Count == 1) 
            {
                // SimplePolygon without holes
                return SimplePolygonWithoutHolesFactory.CreatePolygon(geometry.coordinates);
            }
            else                                
            {
                // SimplePolygon with holes
                return SimplePolygonWithHolesFactory.CreatePolygon(geometry.coordinates);
            }
        }

        return null;
    }
}
