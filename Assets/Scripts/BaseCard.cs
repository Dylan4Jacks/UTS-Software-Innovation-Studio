using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCard {
    public Sprite sprite;
    public string spriteType;
    public string cardName;
    public string description;
    public int strength;
    public int speed;
    public int health;
    public int shield;
    public string[] ability;

    public BaseCard(string name, string description, int strength, int speed, int health, string spriteType) {
        this.cardName = name;
        this.description = description;
        this.strength = strength;
        this.speed = speed;
        this.health = health;
        this.shield = 0;
        this.ability = new string[] {"shield", "right", "1"};

        RuntimeResources resources = RuntimeResources.Instance;
        if (resources.spriteDictionary.TryGetValue(spriteType, out this.sprite)) {
            this.spriteType = spriteType;
        } else {
            Debug.Log("[ERROR]: No such sprite type as \"" + spriteType + "\". Defaulting to wug");
            this.spriteType = "wug";
            this.sprite = resources.spriteDictionary["wug"];
        }
    }
}
