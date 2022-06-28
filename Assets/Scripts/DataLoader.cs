using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private string json;                // Path to json data

    // Start is called before the first frame update
    void Start()
    {
        json = File.ReadAllText(Application.dataPath + "/Data/custom.geo.json");
    }

    public Root GetData()
    {
        return JsonConvert.DeserializeObject<Root>(json);
    }
}
