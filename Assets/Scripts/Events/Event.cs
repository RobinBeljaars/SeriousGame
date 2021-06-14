using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public string title = "Event titel";
    public string desc = "Event beschrijving";
    public int minAge = 0;
    public int maxAge = 100;
    public List<EventOption> options = new List<EventOption>();
}
