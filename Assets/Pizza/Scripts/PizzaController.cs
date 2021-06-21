using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PizzaController : MonoBehaviour
{
    
    public int speed = 5;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().getScore();
            Destroy(gameObject);
        }
    }
}
