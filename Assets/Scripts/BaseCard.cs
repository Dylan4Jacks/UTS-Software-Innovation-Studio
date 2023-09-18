using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCard {
    public string cardName;
    public int strength;
    public int speed;
    public int health;
    public int sheild;

    public BaseCard(string name, int strength, int speed, int health) {
        this.cardName = name;
        this.strength = strength;
        this.speed = speed;
        this.health = health;
        this.sheild = 0;
    }
}
