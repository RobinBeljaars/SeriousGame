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
    public float maxHappiness=100;
    public float startingReputation = 0; 
    public float startingEducation = 0; 
    public float maxEducation=100;
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
    private int eventsExperiencedThisDay =0;

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
        age = age + amount;
        eventsExperiencedThisDay=0;
    }

    public void SetCharacterNickName(string nickName){
        this.nickName=nickName;
    }

    public void SetAvatar(Sprite avatar){
        this.avatar=SpriteData.FromSprite(avatar);
    }

    public void AlterMoney(float amount){
        money=money+amount;
    }

    public void AlterEnergy(float amount){
      float newEnergy = energy+amount;
        if(newEnergy<0){
            energy=0;
        }
        else if(newEnergy>startingEnergy){
                energy = startingEnergy;
        }
        else
        {
            energy=newEnergy;
        }
        
    }
     public void RestoreEnergy(){
         energy=startingEnergy;
     }

    public void AlterHappiness(float amount){
        float newHappiness = happiness+amount;
        if(newHappiness<0){
            happiness=0;
        }
        else if(newHappiness>maxHappiness){
                happiness = maxHappiness;
        }
        else
        {
            happiness=newHappiness;
        }
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

    public float GetReputation(){
        return reputation;
    }

    public void AlterReputation(float amount){
         reputation = reputation + amount;
    }
    public float GetEducation(){
        return education;
    }

    public void AlterEducation(float amount){
         float newEducation = education+amount;
        if(newEducation<0){
            education=0;
        }
        else if(newEducation>maxEducation){
                education = maxEducation;
        }
        else
        {
            education=newEducation;
        }
    }
    
    
    public void IncrementEventsExperiencedThisDay(){
        eventsExperiencedThisDay++;
    }
    public int GetEventsExperiencedThisDay(){
        return eventsExperiencedThisDay;
    }
}
