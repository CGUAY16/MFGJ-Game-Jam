using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrack_Spawner : MonoBehaviour
{
    [SerializeField] GameObject obstacle1;
    [SerializeField] Transform spawnerPos;

    private float spawnCD;
    private float nextSpawnTime;

    bool spawnerReady() => Time.time >= nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnCD = Random.Range(1, 5);
        if (spawnerReady())
        {
            nextSpawnTime = Time.time + spawnCD;
            Instantiate(obstacle1, spawnerPos.position, Quaternion.identity);
        }
    }
}
