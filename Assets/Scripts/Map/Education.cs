using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Education{
    public string Grade;
    public Color colour;
    [Range(0.0f, 100.0f)]
    public float educationMinValue;
    
    [Range(0.0f, 100.0f)]
    public float educationMaxValue;
}