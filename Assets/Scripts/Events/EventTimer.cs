using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxEventsPerYear;
    public bool easyStart;

    public int maxAgeEasyStart;
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
            //Check if a sceneario is currently active, we don't want event & scenario's  active at the same time..
            if(!Game.currentGame.GetScenarioStatus()){
                //Check if an event is allready taking place, if not we don't want a new one
                if(!Game.currentGame.GetEventStatus()){
                        
                    //Check if the maxium is reached or not    
                    if(maxEventsPerYear>Game.currentGame.PlayerData.GetEventsExperiencedThisDay()){
                        //With 0 energy we don't want new events spawning
                        if(Game.currentGame.PlayerData.getEnergy()!=0){

                            bool skipEvent=false;
                            if(easyStart&&Game.currentGame.PlayerData.GetAge()<=maxAgeEasyStart){
                                //A 50 % check will now occur if the event actually happens, this will slow down events
                                if(Random.Range(1,3)==1){
                                    skipEvent=true;
                                }
                            }
                            if(!skipEvent){
                            AudioController.Instance.PlayNotification();
                            eventManager.GenerateEvent();
                            }
                            
                        }
                    }
                }  
            }
        }
    }
    private void setNewTimer(){
        timer = Random.Range(minSecondsBetweenEvents, maxSecondsBetweenEvents);
    }
}
