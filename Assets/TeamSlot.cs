using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    public string name;
    public int attack;
    public int speed;
    public int health;

    public Card(string name, int attack, int speed, int health) {
        this.attack = attack;
        this.speed = speed;
        this.health = health;
    }
}

public class TeamSlot : MonoBehaviour
{
    private Card? baseCard;
    public int currentHealth;
    public int currentAttack;
    public int currentSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool hasCard() {
        return (baseCard != null);
    }

    public void takeDamage(int damage) {
        this.currentHealth -= damage;
    }
    public void dealDamage(TeamSlot defender) {
        defender.takeDamage(this.currentAttack);
        this.takeDamage(defender.currentAttack);
    }
}


