using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountriesGenerator : MonoBehaviour
{
    public DataLoader loader;
    public Material material;

    private Root root;

	private GameObject countries;				// Father of all the countries

    // Start is called before the first frame update
    void Start()
    {
        root = loader.GetData();

        countries = new GameObject("Countries");

        GenerateCountries();
    }

    private void GenerateCountries()
    {
        foreach(Feature feature in root.features)
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
		country.GetComponent<Country>().polygon = PolygonFactory.CreatePolygon(feature.geometry);

        if (feature.geometry.type.Equals("MultiPolygon"))
        {
            return country;
        }

        DrawCountry(country.GetComponent<Country>());

        return country;
    }

    // Generate a mesh and a collider for a country
    private void DrawCountry(Country country)
    {
        // Generating mesh
        Mesh mesh = MeshGenerator.CreateMesh(country.polygon);
        country.GetComponent<MeshRenderer>().material = material;
        country.GetComponent<MeshFilter>().mesh = mesh;

        // Generating collider

    }

}
