using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePolygonWithHolesFactory : PolygonFactory
{
    public static Polygon CreatePolygon(List<List<List<float>>> coordinates)
    {
        return (Polygon)new SimplePolygonWithHoles(coordinates);
    }
}
