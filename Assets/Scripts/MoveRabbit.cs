using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveRabbit : MonoBehaviour
{
    // Start is called before the first frame update
    public int hunger = 100;
    public int drink = 10;
    public float speed = 10.0f;
    public int power = 100;
    public float radius = 20.0f;  
    public float waterWallRadius = 10.0f;
    public float jump = 5.0f;
    public float timerRotate = 2.5f;
    public float timerRest = 1.5f;
    public float timerDrinking = 1.5f;
    public int starvingFood = 20;
    public int starvingWater = 25;
    private float _timerRest = 0;
    private float _timerDrinking = 0;
    private float _timerRotate = 0;
    private GameObject Food, Water;
    private bool isHunger = false;
    private bool isDrink = false;
    private bool isHitWaterWall = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        _timerRotate += Time.fixedDeltaTime;
        if (power <= 0)
        {
            _timerRest += Time.fixedDeltaTime;
            if (_timerRest >= timerRest)
            {
                power += Random.Range(15, 40);
                _timerRest = 0;
            }
        }
        else if (!isHunger && !isDrink)
        {
            if (!isHitWaterWall)
            {Move();}
            else
            {
                Rotate(90, 120);
                Move();
                Move();
                isHitWaterWall = false; // Дабы не утяжелять прогу дополнительным массивом коллайдеров, я сляпал этот костыль
            }
        }
        else if (isHunger)
        {
            if (Food != null)
            {
                transform.LookAt(Food.transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.Translate(Vector3.up * jump * Time.deltaTime);
            }
            else isHunger = false;
        }
        else if (isDrink && !isHitWaterWall)
        {
            // Debug.Log(isDrink);
            if (Water != null)
            {
                transform.LookAt(Water.transform.position);
                Move();
            }
            // else isDrink = false;
            
        }

        if (_timerRotate >= timerRotate && !isHunger)
        {
            Rotate();
            _timerRotate = 0;
            power -= 10;
            hunger -= 3;
            drink -= 5;
        }

        if (hunger <= starvingFood)
        {
            FindFood();
        }

        if (drink <= starvingWater)
        {
            FindWater();
        }
        CheckPower();
    }

    void CheckPower()
    {
        if (power > 100) power = 100;
        if (power < 0) power = 0;
    }

    void Rotate()
    {
        int choose = Random.Range(0, 2);
        if(choose == 0) transform.Rotate(Vector3.up, Random.Range(0, 121));
        else transform.Rotate(Vector3.up, Random.Range(-120, 1));
    }

    void Rotate(int x, int y)               // Метод для вращения с определенных по конечные углы
    {
        int choose = Random.Range(0, 2);
        if(choose == 0) transform.Rotate(Vector3.up, Random.Range(x, y+1));
        else transform.Rotate(Vector3.up, Random.Range(-y, -x+1));
    }

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(Vector3.up * jump * Time.deltaTime);
        power -= 0;
        hunger -= 0;
        drink -= 0;
    }

    bool FindFood()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "FoodForRabbit")
            {
                Food = hitCollider.gameObject;
                isHunger = true;
                // Debug.Log(Food.transform.position);
                return true;
            }
        }

        return false;
    }

    bool FindWater()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Water")
            {
                // Debug.Log(hitCollider);
                Water = hitCollider.gameObject;
                isDrink = true;
                // Debug.Log(isDrink);
                
            }
        }

        Collider[] waterWallColliders = Physics.OverlapSphere(transform.position, waterWallRadius);
        // bool k = false;
        foreach (var waterWallCollider in waterWallColliders)
        {
            if (waterWallCollider.transform.tag == "WaterWall")
                {
                    // Debug.Log(isHitWaterWall + " wall");
                    isHitWaterWall = true;
                    // k = true;
                }
        }
        // if (k == false)
        // {
        //     isHitWaterWall = false;
        //     k = false;
        // }
        return true; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (isHunger && other.transform.tag == "FoodForRabbit")
        {
            Destroy(other.gameObject);
            isHunger = false;
            hunger += Random.Range(20, 40);
        }



        if (isDrink && other.transform.tag == "Water")
        {
            _timerDrinking += Time.fixedDeltaTime;
            // transform.Rotate(Vector3.up, 90);
            
            if (_timerDrinking >= timerDrinking)
            {
                drink += Random.Range(30, 50);
                if (drink >= 50)
                {
                    isDrink = false;
                    // Debug.Log(isDrink);
                }
                _timerDrinking = 0;
                

            }


            
        }
    }


    private void MoveToFoodWater()
    {
        
    }
}
