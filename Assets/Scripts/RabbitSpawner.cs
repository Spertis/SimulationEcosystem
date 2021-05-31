using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Rabbit;
    private int countRabbit;
    void Start()
    {
        countRabbit = UIManager.countRabbits;
        CreateRabbit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateRabbit()
    {
        for (int i = 0; i < countRabbit; ++i)
        {
            int x = 0, z = 0;
            GetRandomCoord(ref x, ref z);
            Instantiate(Rabbit, new Vector3(x, 1, z), Quaternion.identity);
        }
    }

    void GetRandomCoord(ref int x, ref int z)
    {
        x = Random.Range(-200, 200);
        z = Random.Range(-200, 200);
    }
	

}
