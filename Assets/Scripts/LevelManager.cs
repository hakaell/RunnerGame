using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject collectablePrefab;
    public GameObject obstacklePrefab;


    public void SpawnCoins()
    {
        for (int i = 0; i < 5; i++)
        {
            //orta
            Instantiate(collectablePrefab, new Vector3(0, 0, i), Quaternion.identity);
            
        }
        for (int i = 0; i < 5; i++)
        {
            //ensağ
            Instantiate(collectablePrefab, new Vector3(1.5f, 0, i+18.22f), Quaternion.identity);
            
        }
        for (int i = 0; i < 5; i++)
        {
            //ensol
            Instantiate(collectablePrefab, new Vector3(-2f, 0, i+10f), Quaternion.identity);
            Instantiate(collectablePrefab, new Vector3(-2f, 0, i+22f), Quaternion.identity);
        }
        
        print("coin oluşturuldu");
        
    }
    public void SpawnObstackle()
    {
        Instantiate(obstacklePrefab, new Vector3(0.04f, 0.5f, 8f), Quaternion.identity);
        /*
        //ensağ
        Instantiate(obstacklePrefab, new Vector3(1.72f, 0.5f, 25.61f), Quaternion.identity);
        //orta
        Instantiate(obstacklePrefab, new Vector3(0.04f, 0.5f, 7.54f), Quaternion.identity);
        //ensol
        Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 17.6f), Quaternion.identity);
        Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 35.69f), Quaternion.identity);*/
        
        print("obstackle oluşturuldu");
        
    }

    private void Start()
    {
       // SpawnCoins();
       // SpawnObstackle();
    }
}
