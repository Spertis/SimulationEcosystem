using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static int countRabbits = -1;
    public static int countWolfs = -1;

    [SerializeField] InputField field;


    private void Start()
    {
        // DontDestroyOnLoad(gameObject);
    }

    public void StartSimulation()
    {
        if (Checker())
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    
    public void RabbitCounter()
    {
        if (field.text != null)
        {
            countRabbits = int.Parse(field.text);
            SoulsEater.currentRabbitCount = countRabbits;
        }
    }
    
    public void WolfCounter()
    {
        if (field.text != null)
        {
            countWolfs = int.Parse(field.text);
            SoulsEater.currentWolfCount = countWolfs;
            //Debug.Log(SoulsEater.currentWolfCount);
        }
    }

    private bool Checker()
    {
        if (countRabbits <= 0 && countWolfs <= 0)
        {
            print("Введите число особей");
            return false;
        }
        if (countRabbits < 0)
        {
            print("Введите число зайцев");
            return false;
        }

        if (countWolfs < 0)
        {
            print("Введите количество волков");
            return false;
        }

        return true;
    }
    void Update()
    {
        if (Input.GetKey("return"))  
        {
            StartSimulation();
        }
        if (Input.GetKey("escape"))  
        {
            SceneManager.LoadScene("Exit");
        }
    }
}
