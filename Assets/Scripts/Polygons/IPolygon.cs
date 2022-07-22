using Generator.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.Polygons
{
    public abstract class IPolygon<T>
    {
       public PolygonType type { get; set; }

       public LinkedList<Point<Vector3>> nodes = new LinkedList<Point<Vector3>>();

       public abstract Vector3[] GetVertices();
    }
}
