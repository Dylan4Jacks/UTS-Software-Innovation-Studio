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
    public int shield;
    public string[] ability;

    public BaseCard(string name, int strength, int speed, int health) {
        this.cardName = name;
        this.strength = strength;
        this.speed = speed;
        this.health = health;
        this.shield = 0;
        this.ability = new string[] {"shield", "right", "1"};
    }
}
