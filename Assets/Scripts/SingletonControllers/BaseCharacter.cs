using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacter
{


    public int startingAge = 12;
    public float startingMoney = 100;
    public float startingEnergy = 100;
    public float startingHappiness = 0;
    public float reputation = 0; //TODO, inmplement this
    public float totalItemValue = 0; //TODO, inmplement this
    public bool hasSucceededHighSchool = true; //TODO, inmplement this
    public bool hasSucceededCollege = false; //TODO, inmplement this


    private SpriteData avatar;
    private string nickName;
    private int age;
    private float money;
    private float energy;
    private float happiness;

    // Start is called before the first frame update
    public BaseCharacter()
    {
        Debug.Log("Calling BaseConst");
        age=startingAge;
        money = startingMoney;
        energy = startingEnergy;
        happiness = startingHappiness;
    }

    public void IncrementAge(int amount){
        this.age = age + amount;
    }

    public void SetCharacterNickName(string nickName){
        this.nickName=nickName;
    }

    public void SetAvatar(Sprite avatar){
        this.avatar=SpriteData.FromSprite(avatar);
    }

    public void IncrementMoney(float amount){
        money=money+amount;
    }

    public void DecrementMoney(float amount){
        money=money-amount;
    }

    public void IncrementEnergy(float amount){
      
            energy=energy+amount;
        
    }
     public void RestoreEnergy(){
         energy=startingEnergy;
     }

    public void DecrementEnergy(float amount){
        if(amount>energy){
            energy=0;
        }
        else
        {
            energy=energy-amount;
        }
    }

    public void IncrementHappiness(float amount){
        happiness=happiness+amount;
        //TODO: Change sprite when happiness changes
    }

    public void DecrementHappiness(float amount){
        happiness=happiness-amount;
        //TODO: Change sprite when happiness changes
    }

    public Sprite getAvatar(){

        return SpriteData.ToSprite(avatar);
    }

    public int GetAge(){
        return age;
    }
    public string getName(){

        if(nickName==null){
            return "";
        }
        return nickName;
    }

    public float getMoney(){
        return money;
    }
    public float getHappiness(){
        return happiness;
    }

    public float getEnergy(){
        return energy;
    
}
}