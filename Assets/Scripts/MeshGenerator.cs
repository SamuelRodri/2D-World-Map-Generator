using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public static Mesh CreateMesh(Polygon polygon)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = polygon.GetVertices();
        //mesh.triangles = EarClipping(mesh.vertices);

        /*mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		//country.GetComponent<PolygonCollider2D>().points = ConvertCoordinatesPolygon(feature.geometry);*/


        return mesh;
    }
}
