using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHangeSprite : MonoBehaviour
{
    public SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        if(Game.currentGame == null){
            Game.currentGame = new Game();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
