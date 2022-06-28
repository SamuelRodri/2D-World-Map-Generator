using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePolygonWithHoles : SimplePolygon
{
    public SimplePolygonWithHoles(List<List<List<float>>> coordinates)
        : base(coordinates)
    {
        Debug.Log("SimplePolygon with Holes");
    }
}
