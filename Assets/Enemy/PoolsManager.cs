using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public GameObject[] Monster;

    List<GameObject>[] pools;

    // Start is called before the first frame update
    void Start()
    {
        pools = new List<GameObject>[Monster.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    void Update()
    {

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (item != null && !item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            // Instantiate a new GameObject only if the pool is empty or all objects are active
            select = Instantiate(Monster[index], transform); 
            pools[index].Add(select);
        }
        return select;
    }
}

