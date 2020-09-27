using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BoidManager manager;
    [SerializeField] GameObject spawnObject;
    [SerializeField] float spawnInterval = 10;
    [SerializeField] int spawnAmount = 1;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<BoidManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            timer = 0;
            for (int i = 0; i < spawnAmount; i++)
            {
                Instantiate(spawnObject);

            }
        }
    }
}
