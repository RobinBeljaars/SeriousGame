using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{

#region Singleton
public static BaseCharacter Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public int startingAge;
    public float startingMoney;
    public float startingEnergy;
    public float startingHappiness;
    public float reputation; //TODO, inmplement this
    public float totalItemValue; //TODO, inmplement this
    public bool hasSucceededHighSchool; //TODO, inmplement this
    public bool hasSucceededCollege; //TODO, inmplement this

    private Sprite avatar;
    private string nickName;
    private int age;
    private float money;
    private float energy;
    private float happiness;

    // Start is called before the first frame update
    void Start()
    {
        age=startingAge;
        money = startingMoney;
        energy = startingEnergy;
        happiness = startingHappiness;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCharacterNickName(string nickName){
        this.nickName=nickName;
    }

    public void SetAvatar(Sprite avatar){
        this.avatar=avatar;
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

    public void DecrementEnergy(float amount){
        energy=energy-amount;
    }

    public void IncrementHappiness(float amount){
        happiness=happiness+amount;
        //TODO: Change sprite when happiness changes
    }

    public void DecrementHappiness(float amount){
        happiness=happiness-amount;
        //TODO: Change sprite when happiness changes
    }
}
