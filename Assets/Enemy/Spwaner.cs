using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spwaner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float timer;
    private float timerLevel1;
    private float timerLevel2;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerLevel1 += Time.deltaTime;
        
        if (timerLevel1 > 0.5f)
        {
            SpawnLevel1();
            timerLevel1 = 0;
        }

        if (timer >= 20)
        {
            timerLevel2 += Time.deltaTime;
            if (timerLevel2 > 2)
            {
                SpawnLevel2();
                timerLevel2 = 0;
            }
        }
        
    }

    void SpawnLevel1()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
    }
    void SpawnLevel2()
    {
        GameObject enemy = GameManager.instance.pool.Get(1);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
    }
}
