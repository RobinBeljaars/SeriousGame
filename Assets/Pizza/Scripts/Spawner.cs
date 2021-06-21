using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePattern;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    // Update is called once per frame
    void Update()
    {
        if (timeBtwSpawn <= 0) 
        {
            int rand = Random.Range(0,obstaclePattern.Length);
            Instantiate(obstaclePattern[rand], obstaclePattern[rand].transform.position, Quaternion.identity, transform);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
