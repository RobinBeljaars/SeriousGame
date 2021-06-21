using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class EventOption
{
    public string title = "OK";
    public float moneyReward = 0;
    public float happinessReward = 0;
    public float energyReward = 0;
    public float itemValueReward = 0;
    public float reputationReward = 0;
    public float educationReward = 0;

    public string feedback;
    private string feedbackResult;
    
    public Rewards.RewardType customRewardType = Rewards.RewardType.NONE;

    public void GainReward(Rewards.RewardType customRewardType = Rewards.RewardType.NONE)
    {
        feedbackResult="";//Reset previousvalue
        if(moneyReward != 0){
            Rewards.SumMoney(moneyReward);
            feedbackResult=feedbackResult+"\nGeld: € "+moneyReward;
        }
        if(energyReward != 0){
            Rewards.SumEnergy(energyReward);
            feedbackResult=feedbackResult+"\nEnergie: "+energyReward;
        }
        if(happinessReward != 0){
            Rewards.SumHappiness(happinessReward);
            feedbackResult=feedbackResult+"\nStemming: "+happinessReward;
        }
        if(itemValueReward != 0){
            Rewards.SumItemValue(itemValueReward);
            feedbackResult=feedbackResult+"\n"+itemValueReward;
        }
        if (reputationReward != 0){
            Rewards.SumReputation(reputationReward);
            feedbackResult=feedbackResult+"\nReputatie: "+reputationReward;
        }
        if (educationReward != 0){
            Rewards.SumEducation(educationReward);
            feedbackResult=feedbackResult+"\nScholing: "+educationReward;
        }
        //Custom event
        Rewards.GainReward(customRewardType);
        Game.currentGame.PlayerData.IncrementEventsExperiencedThisDay();
        Game.currentGame.SetEventNotActive();
        SaveLoadGame.Save();
    }

    public string GetFeedback(){
        return feedback+feedbackResult;
    }
}
