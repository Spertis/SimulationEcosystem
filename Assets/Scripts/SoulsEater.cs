using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsEater : MonoBehaviour
{
    public static Dictionary<string, int> RabbitDeaths = new Dictionary<string, int>
    {
        {"Age", 0},
        {"Starving", 0},
        {"Dehydration", 0},
        {"Murder", 0}
    };
    public static Dictionary<string, int> WolfDeaths = new Dictionary<string, int>
    {
        {"Age", 0},
        {"Starving", 0},
        {"Dehydration", 0}
    };


    public static int rabbitBirths = 0;
    public static int wolfBirths = 0;

    public static int currentRabbitCount = UIManager.countRabbits;
    public static int currentWolfCount = UIManager.countWolfs;

    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
