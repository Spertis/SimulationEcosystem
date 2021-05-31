using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveRabbit : MonoBehaviour
{
    // Start is called before the first frame update
    public int hunger = 100;
    public int drink = 100;
    public float speed = 10.0f;
    public int power = 100;
    public float radius = 20.0f;
    public float jump = 5.0f;
    public float timerRotate = 2.5f;
    public float timerRest = 1.5f;
    public int starving = 20;
    private float _timerRest = 0;
    private float _timerRotate = 0;
    private GameObject Food;
    private bool isHunger = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timerRotate += Time.deltaTime;
        if (power <= 0)
        {
            _timerRest += Time.deltaTime;
            if (_timerRest >= timerRest)
            {
                power += Random.Range(15, 40);
                _timerRest = 0;
            }
        }
        else if (!isHunger)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.up * jump * Time.deltaTime);
        }
        else if (isHunger)
        {
            if (Food != null)
            {
                transform.LookAt(Food.transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.Translate(Vector3.up * jump * Time.deltaTime);
            }
        }

        if (_timerRotate >= timerRotate && !isHunger)
        {
            Rotate();
            _timerRotate = 0;
            power -= 10;
            hunger -= 5;
        }

        if (hunger <= starving)
        {
            FindFood();
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
        if(choose == 0) transform.Rotate(Vector3.up, Random.Range(0, 120));
        else transform.Rotate(Vector3.up, Random.Range(-120, -1));
    }

    bool FindFood()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "FoodForRabbit")
            {
				
                Food = GetComponent(hitCollider)	 ;
                isHunger = true;
                Debug.Log(Food.transform.position);
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHunger && other.transform.tag == "FoodForRabbit")
        {
            Destroy(other.gameObject);
            isHunger = false;
            hunger += Random.Range(20, 40);
        }
    }
}
