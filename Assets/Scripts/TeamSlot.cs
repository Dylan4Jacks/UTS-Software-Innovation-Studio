using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamSlot : MonoBehaviour
{
    #nullable enable
    private Card? assignedCard;
    #nullable disable
    public int currentHealth;
    public int currentStrength;
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
        return (assignedCard != null);
    }

    public void takeDamage(int damage) {
        this.currentHealth -= damage;
    }
    public void dealDamage(TeamSlot defender) {
        defender.takeDamage(this.currentStrength);
        this.takeDamage(defender.currentStrength);
    }
}


