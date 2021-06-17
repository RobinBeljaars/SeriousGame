using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHangeSprite : MonoBehaviour
{
    public SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        if(Game.currentGame == null){
            Game.currentGame = new Game();
        }
        if (Game.currentGame.PlayerData.getAvatar() != null)
        {
            render.sprite = Game.currentGame.PlayerData.getAvatar();
        }
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
