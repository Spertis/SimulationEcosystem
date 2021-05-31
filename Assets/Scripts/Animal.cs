using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public abstract class Animal : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int drink;
    public int hunger;
    public int age;
    public int ageCount = 0;
    public int maxAge;
    public int power;
    public int libido;
    public int maxPower;
    public int maxHunger;
    public int maxDrink;
    public int minLibido = 75;
    public int criticalHunger = 30;
    public int criticalDrink = 20;
    public int sex;
    public float radius;
    public float speed;
    public float jump;
    public float timerRotate = 2.5f;
    public float currentTimeRotate;
    public float timerWaiter = 1.5f;
    public float currentTimerWaiter = 0;
    public bool isDrink = false;
    public bool isHunger = false;
    public bool isWaiter = false;
    public bool isReprodaction = false;
    //
    public bool isWater = false;
    //
    public GameObject Target;


    public enum Sex
    {
        Boy = 0, 
        Girl = 1
    }

    public virtual bool Finder(string target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        Target = MinDistanceTarget(hitColliders,target);
            
        if (Target != null) return true;

            return false;
    }

    public virtual GameObject MinDistanceTarget(Collider[] hitColliders,string target)
    {
        float minDistance = 1000f;
        GameObject PreTarget = null;
        float distance = 1001f;
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == target)
            {
                distance = Vector3.Distance(transform.position, hitCollider.gameObject.transform.position);
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    PreTarget = hitCollider.gameObject;
                }
            }
        }

        return PreTarget;
    }

    public virtual void MoveToTarget()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.up * jump * Time.deltaTime);
        }

    }

    public void Checker()
    {
        if (power > maxPower) power = maxPower;
        if (power < 0)
        {
            power = 0;
            isWaiter = true;
        }
        if (drink > maxDrink) drink = maxDrink;
        if (drink < 0) drink = 0;
        if (hunger > maxHunger) hunger = maxHunger;
        if (hunger < 0) hunger = 0;
    }

    public void RandomMove()
    {
        if (!isHunger && !isDrink && !isWaiter && !isReprodaction)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.up * jump * Time.deltaTime);
        }
    }
    public void Rotate()
    {
        if (currentTimeRotate >= timerRotate && !isDrink && !isHunger && !isWaiter)
        {
            int choose = Random.Range(0, 2);
            if (choose == 0) transform.Rotate(Vector3.up, Random.Range(0, 120));
            else transform.Rotate(Vector3.up, Random.Range(-120, -1));
            currentTimeRotate = 0;
        }
        else currentTimeRotate += Time.fixedDeltaTime;
    }

    public virtual void SetStartParams(Animal other)
    {
        sex = Random.Range(0, 2) == 0 
            ? (int) Sex.Boy 
            : (int) Sex.Girl;
        age = Random.Range(0, 2) == 0 
            ? maxAge + maxAge % Random.Range(1, 4) 
            : maxAge - maxAge % Random.Range(1, 4);
        if (other == null)
        {
            speed = Random.Range(0, 2) == 0 
                ? speed + speed % Random.Range(1, 3)
                : speed - speed % Random.Range(1, 3);
            hunger = Random.Range(maxHunger*7/10, maxHunger);
            drink = Random.Range(maxDrink*7/10,maxDrink);
            power = Random.Range(maxPower*7/10,maxPower);
            libido = 0;
        }
    }


    public void WaitSomething()
    {
        if (isWaiter && currentTimerWaiter >= timerWaiter)
        {
            isWaiter = false;
            currentTimerWaiter = 0;
            power += Random.Range(25, 46);
        }
        else if (isWaiter) currentTimerWaiter += Time.fixedDeltaTime;
    }

    public void AgeStep() => ageCount++;
    public void EditHunger() => hunger -= Random.Range(4, 8);
    public void EditPower() => power -= Random.Range(3, 8);
    public void EditDrink() => drink -= Random.Range(2, 5);
    private void EditLibido() 
    {
        if (hunger >= maxHunger / 2 && drink >= maxDrink / 2)
        {
            libido += 5;
        }
    }

    public virtual void RotationCheker()
    {
        if (transform.rotation.z != 0 || transform.rotation.x != 0)
        {
            transform.RotateAround(transform.position,Vector3.forward,-transform.rotation.z);
            transform.RotateAround(transform.position,Vector3.right,-transform.rotation.x);
        }

    }

    public virtual void Wallcheker()
    {
        if (transform.position.z <= -205 || transform.position.z >= 205 || transform.position.x <= -205 || transform.position.x >= 205)
        {
            transform.LookAt(Vector3.zero);
        }
        
    }
}
