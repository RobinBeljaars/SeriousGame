using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpongeController : MonoBehaviour
{
    public int speed = 5;
    Rigidbody rb;
    public UnityEvent onPickUpCollision;
    Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void Move(Vector3 movement)
    {
        movement.Normalize();
        rb.velocity = movement * speed;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Stain"))
        {
            AudioController.Instance.PlaySpongePickedUp();
            other.gameObject.SetActive(false);
            onPickUpCollision.Invoke();
        }
    }
}
