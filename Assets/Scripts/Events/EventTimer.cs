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
        timer = minSecondsBetweenEvents;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0){
            timer = timer - Time.deltaTime;
        }
        else{
            timer = 5;
            eventManager.GenerateEvent();
            
        }
    }
}
