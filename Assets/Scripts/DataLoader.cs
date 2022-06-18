using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private string json;

    // Start is called before the first frame update
    void Start()
    {
        json = File.ReadAllText(Application.dataPath + "/Data/countries_data.geo.json");
        InitializeCountries(json);
    }

    private void InitializeCountries(string json)
    {
        Root countries = JsonConvert.DeserializeObject<Root>(json);

        Debug.Log(countries.features[0]);
    }
}
