using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCarousel : MonoBehaviour
{
    public List<Sprite> sprites;
    public Text error;
    public Text energyValue;

    private GameObject avatarImage;
    private int location=0;
    private float energyChoice =0;

    private string characterName="";
    // Start is called before the first frame update
    void Start()
    {
        //In case no game exist.
        if(Game.currentGame==null){
            Game.currentGame = new Game();
        }  

        //Setting reference to image
        avatarImage = GameObject.Find ("AvatarImages");
        Debug.Log("Added sprites to list: "+sprites.Count);

		//Setting the first images by default
		avatarImage.GetComponent<Image>().sprite = sprites[location];

  
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveRight(){
        if(location<sprites.Count-1){
            location++;
        } else{
            location=0;
        }
        avatarImage.GetComponent<Image>().sprite = sprites[location];
    }

    public void MoveLeft(){
        if(location>0){
            location--;
            
        }else{
            location = sprites.Count-1;
        }
        avatarImage.GetComponent<Image>().sprite = sprites[location]; 
    }

    public void ReadStringInput(string characterName){
        this.characterName=characterName;
    }

    public void UpdateEnergyChoice(float amount){
        energyChoice=(int)Math.Round(amount,0);
        energyValue.text="Gewenste speelduur: "+energyChoice+"\nHoe hoger dit getal, hoe langer het duurt tot je ouder wordt";
    }
    

    public bool SetCharacter(){
        if(characterName!=""){
            if(energyChoice==0){
                energyChoice=100;
            }
            Game.currentGame.PlayerData.SetStartingEnergy(energyChoice);
            Game.currentGame.PlayerData.SetAvatar(avatarImage.GetComponent<Image>().sprite);
            Game.currentGame.PlayerData.SetCharacterNickName(characterName);
            Game.currentGame.StartGame();
            SaveLoadGame.game=Game.currentGame;

            return true;
            }
            error.text ="Kies eerst een bijnaam!";
            return false;
    }
}
