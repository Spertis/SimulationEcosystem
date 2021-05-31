using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DataGiver : MonoBehaviour
{
    public Text text;
    
    public string chooser;
    // Start is called before the first frame update
    void Start()
    {
        SetParams( chooser);   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] TextField field;

    public void SetParams(string chooser)
    {
       if (chooser == "Rabbit Age") text.text = "Смерть от старости: " + SoulsEater.RabbitDeaths["Age"].ToString(); 
       if (chooser == "Rabbit Starving") text.text = "Смерть от голода: " + SoulsEater.RabbitDeaths["Starving"].ToString(); 
       if (chooser == "Rabbit Dehydration") text.text =  "Смерть от обезвоживания: " + SoulsEater.RabbitDeaths["Dehydration"].ToString();
       if (chooser == "Rabbit Murder") text.text = "Смерть от волков: " + SoulsEater.RabbitDeaths["Murder"].ToString();
       if (chooser == "Wolf Age") text.text = "Смерть от старости: " + SoulsEater.WolfDeaths["Age"].ToString(); 
       if (chooser == "Wolf Starving") text.text = "Смерть от голода: "  + SoulsEater.WolfDeaths["Starving"].ToString(); 
       if (chooser == "Wolf Dehydration") text.text = "Смерть от обезвоживания: " + SoulsEater.WolfDeaths["Dehydration"].ToString();
       if (chooser == "rabbitBirths") text.text = "Родившиеся кролики: " +  SoulsEater.rabbitBirths.ToString();
       if (chooser == "wolfBirths") text.text = "Родившиеся волки: " +  SoulsEater.wolfBirths.ToString();
       if (chooser == "currentRabbitCount") text.text = "Текущее количество кроликов: " +  SoulsEater.currentRabbitCount.ToString(); 
       if (chooser == "currentWolfCount") text.text = "Текущее количество волков: " + SoulsEater.currentWolfCount.ToString();
    }
    
}
