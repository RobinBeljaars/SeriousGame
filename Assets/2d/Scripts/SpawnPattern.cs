using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPattern : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(obj, transform.position, Quaternion.identity, gameObject.GetComponentInParent<Transform>()); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
