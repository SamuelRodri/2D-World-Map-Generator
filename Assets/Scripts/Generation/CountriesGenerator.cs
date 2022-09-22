using Generator.Data;
using TriangleNet.Geometry;
using UnityEngine;


namespace Generator.Generators
{
    public class CountriesGenerator : MonoBehaviour
    {
        [Header("Data")]
        [Tooltip("Loads the countries data from a geojson")]
        public DataLoader loader;
        public Material material;
        private Root root;
        private GameObject countries;               // Father of all the countries

        // Start is called before the first frame update
        void Start()
        {
            root = loader.GetData();
            countries = new GameObject("Countries");
            GenerateCountries();
        }

        private void GenerateCountries()
        {
            foreach (Feature feature in root.features)
            {
                GameObject countryGameObject = CreateCountry(feature);
                countryGameObject.transform.parent = countries.transform;
            }
        }

        private GameObject CreateCountry(Feature feature)
        {
            GameObject country = new GameObject(feature.properties.admin);
            country.AddComponent<Country>();

            // Adding data to the country
            country.GetComponent<Country>().admin = feature.properties.admin;
            country.GetComponent<Country>().ISO_A3 = feature.properties.iso_a3;
            country.GetComponent<Country>().polygon = PolygonGenerator.CreatePolygon(feature.geometry);

            MeshGenerator.CreateMesh(country.GetComponent<Country>(), country.GetComponent<Country>().polygon, material);
            return country;
        }
    }
}