using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPrefab : Animal
{
    void Start()
    {
        SetStartParams(null);
        InvokeRepeating("AgeStep", 0, 60f);
        InvokeRepeating("EditDrink", 0, 2.5f);
        InvokeRepeating("EditHunger", 0, 3f);
        InvokeRepeating("EditPower", 0, 1.2f);
        InvokeRepeating("EditLibido", 0, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsAlive();
        RandomMove();
        Rotate();
        RotationCheker();
        Wallcheker();
        WaitSomething();
        Checker();
        if (hunger < criticalHunger) isHunger = Finder("Rabbit");
        else if (drink < criticalDrink) isDrink = Finder("Water");
        else if (libido >= minLibido) isReprodaction = Finder("Wolf");
        MoveToTarget();
    }

    public override bool Finder(string target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        if (target != "Wolf")
        {
            Target = MinDistanceTarget(hitColliders, target);
            if (Target != null) return true;
        }

        if (target == "Wolf")
        {
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == "Wolf")
                {
                    var a = hitCollider.gameObject;
                    if (a.GetComponent<WolfPrefab>().sex != sex
                        && a.GetComponent<WolfPrefab>().libido >= a.GetComponent<WolfPrefab>().minLibido)
                    {
                        Target = a;
                        return true;
                    }
                }
            }
        }
        return false;
    }


    public void SetStartParams(WolfPrefab other)
    {
        jump = 0;
        if (other == null) base.SetStartParams(other);
        else
        {
            other.speed = (other.speed + speed) / 2;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isHunger && other.transform.tag == "Rabbit")
        {
            SoulsEater.RabbitDeaths["Murder"]++;
            SoulsEater.currentRabbitCount--;
            Destroy(other.gameObject);
            hunger += Random.Range(60, 90);
            isHunger = false;
            isWaiter = true;
        }
        else if (isReprodaction && other.transform.tag == "Wolf")
        {
            WolfPrefab wolf = other.gameObject.GetComponent<WolfPrefab>();
            if (wolf.sex != sex)
            {
                isWaiter = true;
                libido = 0;
                isReprodaction = false;
                Target = null;

                if (sex == (int) Sex.Girl)
                {
                    Instantiate(wolf, wolf.transform.position, Quaternion.identity);
                    SetStartParams(wolf);
                    SoulsEater.wolfBirths++;
                    SoulsEater.currentWolfCount++;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDrink && other.transform.tag == "Water")
        {
            Target = null;
            drink += Random.Range(50, 70);
            isDrink = false;
            isWaiter = true;
        }
    }

    public void IsAlive()
    {
        if (ageCount >= age)
        {
            SoulsEater.WolfDeaths["Age"]++;
            SoulsEater.currentWolfCount--;
            Destroy(gameObject);
        }

        if (hunger == 0)
        {
            SoulsEater.WolfDeaths["Starving"]++;
            SoulsEater.currentWolfCount--;
            Destroy(gameObject);
        }

        if (drink == 0)
        {
            SoulsEater.WolfDeaths["Dehydration"]++;
            SoulsEater.currentWolfCount--;
            Destroy(gameObject);
        }

    }

    
}