using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePolygon : Polygon
{
    public SimplePolygon(List<List<List<float>>> coordinates)
    {
        for (int i = 0; i < coordinates.Count; i++) // Coordinates only has one item
        {
            float x = coordinates[0][i][0];
            float y = coordinates[0][i][1];

            AddNode(new Vector3(x, y));
        }
    }
}
