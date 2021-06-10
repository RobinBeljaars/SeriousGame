using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCarousel : MonoBehaviour
{
    public List<Sprite> sprites;
    public Text error;
    private GameObject avatarImage;
    private int location=0;

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
        Debug.Log("Text: "+characterName);
    }

    public bool SetCharacter(){
        if(characterName!=""){
            Debug.Log("Charachter can be set");
        Game.currentGame.PlayerData.SetAvatar(avatarImage.GetComponent<Image>().sprite);
        Game.currentGame.PlayerData.SetCharacterNickName(characterName);
        SaveLoadGame.game=Game.currentGame;
        return true;
        }
        error.text ="Kies eerst een bijnaam!";
        return false;
    }
}
