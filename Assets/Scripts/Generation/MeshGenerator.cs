using Generator.Data;
using Generator.EarClipping;
using Generator.Polygons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.Generation
{
    public class MeshGenerator : MonoBehaviour
    {
        public Material material;

        public void CreateMesh(Country country, IPolygon<Vector3> polygon)
        {
            if(polygon == null)
            {
                return;
            }

            Mesh mesh = new Mesh();

            mesh.vertices = polygon.GetVertices();
            mesh.triangles = Triangulator.EarClipping(polygon);

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            country.GetComponent<MeshRenderer>().material = material;
            country.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}