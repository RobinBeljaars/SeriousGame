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
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) {
            other.gameObject.SetActive(false);
            onPickUpCollision.Invoke();
        }
    }
}