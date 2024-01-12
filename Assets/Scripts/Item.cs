using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Temperature, Humidity, Heater, Ventilation, Light, Blind, Nutrients
}
[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
}
