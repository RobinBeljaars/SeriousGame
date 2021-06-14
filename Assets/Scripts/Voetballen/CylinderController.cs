using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CylinderController : MonoBehaviour
{
    public float speed;
    public UnityEvent onPickUpCollision;

    private Rigidbody rb;

    
    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void Move(Vector3 movement)
    {
        rb.MovePosition(transform.position + movement * speed);
    }
}
