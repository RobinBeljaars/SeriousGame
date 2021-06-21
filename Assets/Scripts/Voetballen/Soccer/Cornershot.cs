using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornershot : MonoBehaviour
{
    public Transform target;
    public float force = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        // check if collision is ball
        if(other.CompareTag("Ball")){
            Vector3 positionBall = other.gameObject.transform.position;
            Vector3 to = target.position - positionBall;
            other.gameObject.GetComponent<SoccerBall>().Kick(to, force);
            // kick ball back to center
        }
    }
}
