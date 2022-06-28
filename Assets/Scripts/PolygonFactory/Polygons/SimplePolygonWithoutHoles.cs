using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePolygonWithoutHoles : SimplePolygon
{
    public SimplePolygonWithoutHoles(List<List<List<float>>> coordinates)
        : base(coordinates)
    {
        Debug.Log("SimplePolygon without Holes");
    }
}
