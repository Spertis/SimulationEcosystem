using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Returner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReturnToStart()
    {
        DeleteParams();
        SceneManager.LoadScene("StartInterface");
    }
    
    public void DeleteParams()
    {
        SoulsEater.RabbitDeaths["Age"] = 0;
        SoulsEater.RabbitDeaths["Starving"] = 0;
        SoulsEater.RabbitDeaths["Dehydration"] = 0;
        SoulsEater.RabbitDeaths["Murder"] = 0;
        SoulsEater.WolfDeaths["Age"] = 0;
        SoulsEater.WolfDeaths["Starving"] = 0;
        SoulsEater.WolfDeaths["Dehydration"] = 0;
        SoulsEater.rabbitBirths = 0;
        SoulsEater.wolfBirths = 0;
        // SoulsEater.currentRabbitCount = -1;
        // SoulsEater.currentWolfCount = -1;
    }
}
