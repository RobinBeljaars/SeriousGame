using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Mood{
    public string mood;
    public Color colour;
    [Range(0.0f, 100.0f)]
    public float happinessMinValue;
    
    [Range(0.0f, 100.0f)]
    public float happinessMaxValue;
}