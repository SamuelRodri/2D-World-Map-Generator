using Generator.Data;
using Generator.Generation;
using Generator.Polygons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CountriesGenerator : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Loads the countries data from a geojson")]
    public DataLoader loader;
    public MeshGenerator meshGenerator;
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
        country.GetComponent<Country>().polygon = PolygonFactory.CreatePolygon(feature.geometry);

        DrawCountry(country.GetComponent<Country>(), country.GetComponent<Country>().polygon);

        return country;
    }

    // Generate a mesh and a collider for a country
    private void DrawCountry(Country country, IPolygon<Vector3> polygon)
    {
        if(polygon == null)
        {
            return;
        }

        #region CreatingMesh
        // Generating mesh
        if (polygon.type == PolygonType.SimplePolygon)
        {
            // Simple Polygon
            meshGenerator.CreateMesh(country, polygon);

        }else if(polygon.type == PolygonType.SimplePolygonWithHoles)
        {
            // Simple Polygon With Holes
            meshGenerator.CreateMesh(country, polygon);
        }
        else if(polygon.type == PolygonType.MultiPolygon)
        {
            // Multi Polygon
            MultiPolygon multiPolygon = (MultiPolygon)polygon;
            for(int i = 0; i < multiPolygon.polygons.Count; i++)
            {
                GameObject partOfCountry = new GameObject();
                partOfCountry.AddComponent<Country>();

                meshGenerator.CreateMesh(partOfCountry.GetComponent<Country>(), (SimplePolygon)multiPolygon.polygons[i]);
                partOfCountry.transform.parent = country.gameObject.transform;
            }

            MeshFilter[] meshFilters = country.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int k = 0;
            while (k < meshFilters.Length)
            {
                combine[k].mesh = meshFilters[k].sharedMesh;
                combine[k].transform = meshFilters[k].transform.localToWorldMatrix;
                meshFilters[k].gameObject.SetActive(false);

                k++;
            }

            country.GetComponent<MeshFilter>().mesh = new Mesh();
            country.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            country.GetComponent<MeshRenderer>().material = material;
            country.gameObject.SetActive(true);

        }
        else if(polygon.type == PolygonType.MultiPolygonWithHoles)
        {
            // Multi Polygon With Holes
           
        }
        #endregion

        // Generating collider

    }

}

