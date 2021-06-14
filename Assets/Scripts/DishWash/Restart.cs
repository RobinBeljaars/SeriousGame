using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public NavigationScript navigationScript;

    public Text Wintext;

    public float moneyReward;
    public float energyCost;
    // Start is called before the first frame update
    void Start()
    {
            Wintext.text="Gefeliciteerd, het aanrecht is weer schoon!\nJe hebt "+moneyReward+" euro gekregen van je ouders en de moeite heeft je "+energyCost+" energiepunten gekost.\nDruk op R om terug te gaan!";
            Game.currentGame.PlayerData.AlterMoney(moneyReward);
            Game.currentGame.PlayerData.AlterEnergy(energyCost);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            navigationScript.SwitchScreen();
        }
    }
}
