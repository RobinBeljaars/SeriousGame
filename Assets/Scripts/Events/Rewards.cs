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
        PlayerProperties.money += amount;
    }

    public static void SumHappiness(float happiness = 10)
    {
        PlayerProperties.happiness += happiness;
    }

    public static void SumEnergy(float energy = 10)
    {
        PlayerProperties.energy += energy;
    }

    public static void SumItemValue(float itemValue = 1)
    {
        PlayerProperties.totalItemValue += itemValue;
    }

    public static void SumReputation(float reputation = 5)
    {
        PlayerProperties.reputation += reputation;
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
