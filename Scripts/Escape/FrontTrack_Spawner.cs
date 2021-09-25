using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTrack_Spawner : MonoBehaviour
{
    [SerializeField] GameObject obstacle2;
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
        spawnCD = Random.Range(2f, 4f);
        if (spawnerReady())
        {
            nextSpawnTime = Time.time + spawnCD;
            Instantiate(obstacle2, spawnerPos.position, Quaternion.identity);
        }
    }
}
