using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCard {
    GameObject  creatureDisplay; // this is a GameObject incase we want to add accessories to the wug
    public string cardName;
    public int strength;
    public int speed;
    public int health;

    public BaseCard(string name, int strength, int speed, int health) {
        this.cardName = name;
        this.strength = strength;
        this.speed = speed;
        this.health = health;
    }
}
