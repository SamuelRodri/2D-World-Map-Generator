using Generator.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TriangleNet.Geometry;
using UnityEngine;

namespace Generator.Generators
{
    public static class MeshGenerator
    {
        public static void CreateMesh(Country country, Polygon polygon, Material material)
        {
            if(polygon == null)
            {
                return;
            }

            var mesh = polygon.Triangulate();

            #region Unity's Mesh Creation
            var mesh2 = new Mesh
            {
                name = "Procedural Mesh"
            };

            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Vector3> normals = new List<Vector3>();

            foreach (Vertex v in mesh.Vertices)
            {
                vertices.Add(new Vector3((float)v.x, (float)v.y));
            }

            foreach (TriangleNet.Topology.Triangle t in mesh.Triangles)
            {
                triangles.Add(t.vertices[0].id);
                triangles.Add(t.vertices[1].id);
                triangles.Add(t.vertices[2].id);
            }

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                normals.Add(Vector3.back);
            }

            mesh2.vertices = vertices.ToArray();
            triangles.Reverse();
            mesh2.triangles = triangles.ToArray();
            mesh2.normals = normals.ToArray();

            country.GetComponent<MeshFilter>().mesh = mesh2;
            country.GetComponent<MeshRenderer>().material = material;
            #endregion
        }
    }
}