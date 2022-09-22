using TriangleNet.Geometry;
using UnityEngine;

namespace Generator.Data
{
    [RequireComponent(typeof(Country))]
    [RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
    public class Country : MonoBehaviour
    {
        public string admin;
        public string ISO_A3;
        public Polygon polygon;

        public Country(string admin, string iso, Polygon polygon)
        {
            this.admin = admin;
            this.ISO_A3 = iso;
            this.polygon = polygon;
        }
    }
}
