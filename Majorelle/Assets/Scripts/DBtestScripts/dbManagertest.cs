using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class dbManagertest : MonoBehaviour
{
    public string DBurl = "https://fir-test-33256-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        WriteDB();
        ReadDB();
    }


    public void WriteDB()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        GPSdata DATA1 = new GPSdata("seoul", 37.0f, 23.4f, 123f);
        GPSdata DATA2 = new GPSdata("Busan", 137.0f, 12223.4f, 13.5f);
        GPSdata DATA3 = new GPSdata("Dague", 237f, 223.4f, 0.3f);

        string jsondata1 = JsonUtility.ToJson(DATA1);
        string jsondata2 = JsonUtility.ToJson(DATA2);
        string jsondata3 = JsonUtility.ToJson(DATA3);

        reference.Child("Korea").Child("area1").SetRawJsonValueAsync(jsondata1);
        reference.Child("Korea").Child("area2").SetRawJsonValueAsync(jsondata2);
        reference.Child("Korea").Child("area3").SetRawJsonValueAsync(jsondata3);
    }
    public void ReadDB()
    {
        reference = FirebaseDatabase.DefaultInstance.GetReference("Korea");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary GPSdata = (IDictionary)data.Value;
                    Debug.Log("이름: " + GPSdata["name"] + "위도: " + GPSdata["latitude_data"] + "경도: " + GPSdata["longitude_data"]
                        + "고도: " + GPSdata["altitude_data"]);
                }
            }
        }
        );

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class GPSdata
{
    public string name = "";
    public float latitude_data = 0;
    public float longitude_data = 0;
    public float altitude_data = 0;

    public GPSdata(string Name, float Lat, float Lon, float ALT)
    {
        name = Name;
        latitude_data = Lat;
        longitude_data = Lon;
        altitude_data = ALT;
    }
}