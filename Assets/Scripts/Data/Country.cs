using Generator.Polygons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator.Data
{
    [RequireComponent(typeof(Country))]
    [RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
    public class Country : MonoBehaviour
    {
        public string admin;
        public string ISO_A3;
        public IPolygon<Vector3> polygon;

        public Country(string admin, string iso, IPolygon<Vector3> polygon)
        {
            this.admin = admin;
            this.ISO_A3 = iso;
            this.polygon = polygon;
        }
    }
}
