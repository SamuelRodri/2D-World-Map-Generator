using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountriesGenerator : MonoBehaviour
{
    public DataLoader loader;

    private Root root;

    private GameObject countryGameObject;
    private GameObject countries; 

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
            countryGameObject = new GameObject(feature.properties.admin);
            countryGameObject.AddComponent<Country>();
            CreateCountry(countryGameObject, feature.properties.admin, feature.properties.iso_a3);
            countryGameObject.transform.parent = countries.transform;
        }
    }

    private void CreateCountry(GameObject country, string admin, string iso)
    {
        country.GetComponent<Country>().admin = admin;
        country.GetComponent<Country>().ISO_A3 = iso;
    }
}
