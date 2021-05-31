using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject foodPrefab;
    public GameObject waterPrefab;
    private float delaySpawn;
    private int countFood;
    private float reCount;
    private float delayTimer = 0;
    private float reTimer = 0;
    void Start()
    {
        GiveCountFood();
        for (int i = 0; i < countFood; ++i) CreateFood();
        for (int i = 0; i < Random.Range(25,40);++i) CreateWater();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delayTimer += Time.fixedDeltaTime;
        if (delayTimer >= delaySpawn && countFood > 0)
        {
            CreateFood();
            delayTimer = 0;
            delaySpawn = Random.Range(1.0f, 3.0f);
        }

        if (countFood == 0)
        {
            reTimer += Time.fixedDeltaTime;
            if (reTimer >= reCount)
            {
                GiveCountFood();
                reTimer = 0;
            }
        }
    }

    void CreateFood()
    {
        Instantiate(foodPrefab, new Vector3(Random.Range(-200, 200), 0.1f,
                Random.Range(-200, 200)), Quaternion.identity);
        --countFood;
    }

    void GiveCountFood()
    {
        countFood = Random.Range(1000, 2000);
        reCount = Random.Range(5.0f, 30.0f);
    }

    void CreateWater()
    {
        Instantiate(waterPrefab, new Vector3(Random.Range(-200, 200), -0.09f, Random.Range(-200, 200)),
            Quaternion.identity);
    }
}
