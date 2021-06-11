using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacter
{


    public int startingAge = 12;
    public float startingMoney = 100;
    public float startingEnergy = 100;
    public float startingHappiness = 50;
    public float startingReputation = 0; 
    public float startingEducation = 0; 
    public float totalItemValue = 0; //TODO, inmplement this
    public bool hasSucceededHighSchool = true; //TODO, inmplement this
    public bool hasSucceededCollege = false; //TODO, inmplement this


    private SpriteData avatar;
    private string nickName;
    private int age;
    private float money;
    private float energy;
    private float happiness;
    private float education;

    private float reputation;
    // Start is called before the first frame update
    public BaseCharacter()
    {
        Debug.Log("Calling BaseConst");
        age=startingAge;
        money = startingMoney;
        energy = startingEnergy;
        happiness = startingHappiness;
        reputation = startingReputation; 
        education = startingEducation;
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
        float newHappiness = happiness+amount;
        if(newHappiness>100){
            happiness=100;
        }else
        {
            happiness=newHappiness;
        }
        //TODO: Change sprite when happiness changes
    }

    public void DecrementHappiness(float amount){
       if(amount>happiness){
           happiness=0;
       }
       else
       {
           happiness=happiness-amount;
       }
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

    public float GetReputation(){
        return reputation;
    }

    public void IncrementReputationn(float amount){
         reputation = reputation +amount;
    }
     public void DecrementReputationn(float amount){
         reputation = reputation -amount;
    }
    public float GetEducation(){
        return education;
    }

    public void IncrementEducation(float amount){
         float newEducation = education +amount;
         if(newEducation>100){
             education=100;
         }else
         {
             education = newEducation;
         }
    }
     public void DecrementEducation(float amount){
         if(amount>education){
             education=0;
         }else{
             education = education-amount;
         }
    }
}