using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rewards : MonoBehaviour
{
    public enum RewardType
    {
        NONE,
        CUSTOMEVENT,
        SAYSOMETHINGCOOL,
        ANOTHERCUSTOMEVENT
    }


    public static void GainReward(RewardType rewardType = RewardType.NONE)
    {
        switch(rewardType)
        {
            case RewardType.NONE:
                break;
            case RewardType.CUSTOMEVENT:
                CustomEvent();
                break;
            case RewardType.SAYSOMETHINGCOOL:
                SaySomethingCool();
                break;
            case RewardType.ANOTHERCUSTOMEVENT:
                AnotherCustomEvent();
                break;
            default:
                break;
        }
    }

    public static void SumMoney(float amount = 10)
    {
        Game.currentGame.PlayerData.AlterMoney(amount);
    }

    public static void SumHappiness(float happiness = 10)
    {
        Game.currentGame.PlayerData.AlterHappiness(happiness);
    }

    public static void SumEnergy(float energy = 10)
    {
        Game.currentGame.PlayerData.AlterEnergy(energy);
    }

    public static void SumItemValue(float itemValue = 1)
    {
       //NOT IMPLEMENTED
    }

    public static void SumReputation(float reputation = 5)
    {
        Game.currentGame.PlayerData.AlterReputation(reputation);
    }

    public static void SumEducation(float education = 5)
    {
        Game.currentGame.PlayerData.AlterEducation(education);
    }


    //Custom reward
    private static void CustomEvent()
    {
        Debug.Log("Custom reward");
    }

    private static void SaySomethingCool()
    {
        Debug.Log("Something cool");
    }

    private static void AnotherCustomEvent()
    {
        Debug.Log("Another Custom reward");
    }
}
