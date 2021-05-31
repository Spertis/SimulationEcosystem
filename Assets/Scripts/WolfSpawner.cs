using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Wolf;
    private int countWolfs;
    void Start()
    {
        countWolfs = UIManager.countWolfs; 
        CreateWolf();
    }

    void CreateWolf()
    {
        for (int i = 0; i < countWolfs; ++i)
        {
            int x = 0, z = 0;
            GetRandomCoord(ref x, ref z);
            Instantiate(Wolf,
                new Vector3(x, 1, z), Quaternion.identity);
        }
    }
    
    void GetRandomCoord(ref int x, ref int z)
    {
        x = Random.Range(-200, 200);
        z = Random.Range(-200, 200);
    }
}
