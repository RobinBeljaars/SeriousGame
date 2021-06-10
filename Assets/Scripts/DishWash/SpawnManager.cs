using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePattern;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    public void Spawn()
    {
        int rand = Random.Range(0, obstaclePattern.Length);
        Instantiate(obstaclePattern[rand], obstaclePattern[rand].transform.position, Quaternion.identity, transform);
    }
}
