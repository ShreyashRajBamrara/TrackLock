        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_pole : MonoBehaviour
{
    [SerializeField] GameObject train;
    [SerializeField] [Range(0,50)] int pool_size = 5; //size for a wave of 5 enemy /obj in object pool
    [SerializeField] [Range(0.1f,30f)] float spawn_timer = 1f;

    // [SerializeField] Transform[] spawn_points;

    GameObject[] pool;

    void Awake()
    {
        Populate_pool();

    }

    void Populate_pool()
    {
        pool = new GameObject[pool_size];
        for (int i = 0; i < pool.Length; i++)
        {

            pool[i] = Instantiate(train, transform);
            pool[i].SetActive(false);

        }
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Enable_obj_in_pool() // this puts each enemy of the wave one by one
    {
        for (int i = 0; i < pool_size; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {

                pool[i].SetActive(true);
                return;
            }

        }
    }

    IEnumerator SpawnEnemy()// this creates a wave
    {
        while (true) //if not broken ,then this will crash the game if infinite
        {
            Enable_obj_in_pool();
            yield return new WaitForSeconds(spawn_timer);
        }

    }

}
