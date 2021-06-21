using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent unityEvent;
    private Vector2 target;
    private Vector2 start;
    public int yInc;
    public int speed;
    public int hp = 3;
    public GameObject gameOver;
    public GameObject ins;
    public Text HPDisplay;
    public Sprite duckSprite;
    public Sprite defaultSprite;
    private SpriteRenderer spriteRenderer;
    // Update is called once per frame
    private void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
        start = new Vector2(transform.position.x, transform.position.y);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {

        
        HPDisplay.text = "HP: " + hp.ToString();
        if (hp<=0)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, target, speed);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ins.activeSelf)
            {
                ins.SetActive(false);
            }
            else
            {
                ins.SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            target = new Vector2(start.x, start.y + yInc);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            target = new Vector2(start.x, start.y - 1);
            spriteRenderer.sprite = duckSprite;
        }
        else
        {
            target = new Vector2(start.x, start.y);
            spriteRenderer.sprite = defaultSprite;
        }

    }
    public void TakeDamage(int amount)
    {
        hp=hp-amount;
    }
    public void getScore()
    {
        unityEvent.Invoke();
    }
}
