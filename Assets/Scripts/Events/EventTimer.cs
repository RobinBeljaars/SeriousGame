using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxEventsPerYear;
    public int minSecondsBetweenEvents;

    public int maxSecondsBetweenEvents;

    public EventManager eventManager;

    private float timer;

    void Start()
    {
        setNewTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0){
            timer = timer - Time.deltaTime;
        }
        else{

            setNewTimer();

            //Check if an event is allready taking place, if not we don't want a new one
            if(!eventManager.window.activeInHierarchy){

            //Check if the maxium is reached or noet    
            if(maxEventsPerYear>Game.currentGame.PlayerData.GetEventsExperiencedThisDay()){
                    AudioController.Instance.PlayNotification();
                    eventManager.GenerateEvent();
                }
            }  
        }
    }

    private void setNewTimer(){
        timer = Random.Range(minSecondsBetweenEvents, maxSecondsBetweenEvents);
    }
}
