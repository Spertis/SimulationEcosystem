using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class RabbitPrefab : Animal
{
    
    void Start()
    {
        SetStartParams(null);
        InvokeRepeating("AgeStep", 0, 60f);
        InvokeRepeating("EditDrink", 0, 2.5f);
        InvokeRepeating("EditHunger", 0, 3f);
        InvokeRepeating("EditPower", 0, 1f);
        InvokeRepeating("EditLibido", 0, 2f);

    }

    void FixedUpdate()
    {
        IsAlive();
        RandomMove();
        Rotate();
        RotationCheker();
        Wallcheker();
        WaitSomething();
        Checker();
        if (hunger < criticalHunger) isHunger = Finder("FoodForRabbit");
        else if (drink < criticalDrink) isDrink = Finder("Water");
        else if (libido >= minLibido) isReprodaction = Finder("Rabbit");

        MoveToTarget();
    }

     public void SetStartParams(RabbitPrefab other)
    {
        if(other == null) base.SetStartParams(other);
        else
        {
            other.speed = (other.speed + speed) / 2;
        }
    }
     
     public override bool Finder(string target)
     {
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
             if (target != "Rabbit")
             {
                 Target = MinDistanceTarget(hitColliders,target);
                 if (Target != null) return true;
             }
             if ( target == "Rabbit")
             {
                 foreach (var hitCollider in hitColliders)
                 {
                     if (hitCollider.transform.tag == "Rabbit")
                     {
                         var a = hitCollider.gameObject;
                         if (a.GetComponent<RabbitPrefab>().sex != sex
                             && a.GetComponent<RabbitPrefab>().libido >= a.GetComponent<RabbitPrefab>().minLibido)
                         {
                             Target = a;
                             return true;
                         }
                     }
                 }

             }
             return false;
     }
     


    private void OnTriggerStay(Collider other)
    {
        if (isHunger && other.transform.tag == "FoodForRabbit")
        {
            Destroy(other.gameObject);
            Target = null;
            hunger += Random.Range(45, 66);
            isHunger = false;
            isWaiter = true;

        }
        else if (isDrink && other.transform.tag == "Water")
        {
            Target = null;
            drink += Random.Range(30, 50);
            isDrink = false;
            isWaiter = true;

        }
    }

    private void OnCollisionStay(Collision other)
    { 
        if (isReprodaction && other.transform.tag == "Rabbit")
        {
            RabbitPrefab rabbit = gameObject.GetComponent<RabbitPrefab>();
            if (other.gameObject.GetComponent<RabbitPrefab>().sex != sex) 
            {
                isWaiter = true;
                libido = 0;
                isReprodaction = false;
                Target = null;

                if (sex == (int)Sex.Girl)
                {
                    for (int i = 0; i < Random.Range(1, 11); i++)
                    { 
                        Instantiate(rabbit,
                            new Vector3(transform.position.x + Random.Range(-0.7f,0.7f),
                                transform.position.y,
                                transform.position.z +  Random.Range(-0.7f,0.7f)), Quaternion.identity);
                        SoulsEater.rabbitBirths++;
                        SoulsEater.currentRabbitCount++;
                    }
                } 
            }
        }

    }

    public void IsAlive()
    {
        if (ageCount >= age)
        {
            SoulsEater.RabbitDeaths["Age"]++;
            SoulsEater.currentRabbitCount--;
            Destroy(gameObject);
        }

        if (hunger == 0)
        {
            SoulsEater.RabbitDeaths["Starving"]++;
            SoulsEater.currentRabbitCount--;
            Destroy(gameObject);
        }
        if (drink == 0)
        {
            SoulsEater.RabbitDeaths["Dehydration"]++;
            SoulsEater.currentRabbitCount--;
            Destroy(gameObject);
        }
        
    }
}