using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public Rewards.RewardType customRewardType = Rewards.RewardType.NONE;

    public void GainReward(Rewards.RewardType customRewardType = Rewards.RewardType.NONE)
    {
        if(moneyReward != 0)
            Rewards.SumMoney(moneyReward);

        if(happinessReward != 0)
            Rewards.SumHappiness(happinessReward);

        if(energyReward != 0)
            Rewards.SumEnergy(energyReward);

        if(itemValueReward != 0)
            Rewards.SumItemValue(itemValueReward);

        if (reputationReward != 0)
            Rewards.SumReputation(reputationReward);

        if (educationReward != 0)
            Rewards.SumEducation(educationReward);

        //Custom event
        Rewards.GainReward(customRewardType);
        Game.currentGame.PlayerData.IncrementEventsExperiencedThisDay();
        SaveLoadGame.Save();


    }
}
