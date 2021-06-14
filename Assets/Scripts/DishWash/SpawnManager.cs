using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePattern;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, obstaclePattern.Length);
        Instantiate(obstaclePattern[rand], obstaclePattern[rand].transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    public void Spawn()
    {
        GameObject dirt = GameObject.FindGameObjectWithTag("Stain");
        if (dirt == null)
        {
            return;
        }
        int rand = Random.Range(0, obstaclePattern.Length);
        if (dirt.transform.position == obstaclePattern[rand].transform.position)
        {
            obstaclePattern = obstaclePattern.Where((source, index) => index != rand).ToArray();
            Spawn();
        }
        else
        {
            Instantiate(obstaclePattern[rand], obstaclePattern[rand].transform.position, Quaternion.identity, transform);
            Destroy(dirt);
        }
    }
}
