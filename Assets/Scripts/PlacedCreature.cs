using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlacedCreature : MonoBehaviour
{
    private BattleController battleController;
    public BaseCard baseStats;
    public int currentHealth;
    public int currentStrength;
    public int currentSpeed; 
    public bool isSlain;

    [SerializeField] private GameObject displayHealth;
    [SerializeField] private GameObject displayStrength;
    private int alignment;
    private int position;
    
    // Awake is called when instantiated
    void Awake()
    {
        this.battleController = BattleController.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentStrength(int value) {
        currentStrength = value;
        displayStrength.GetComponent<TextMeshPro>().text = value.ToString();
    }
    public void setCurrentHealth(int value) {
        currentHealth = value;
        displayHealth.GetComponent<TextMeshPro>().text = value.ToString();
    }
    public void setCurrentSpeed(int value) {
        //TODO add display speed
        currentSpeed = value;
    }

    public void setupCard(BaseCard baseCard, int alignment, int position) {
        this.baseStats = baseCard;
        setCurrentHealth(baseCard.health);
        setCurrentStrength(baseCard.strength);
        setCurrentSpeed(baseCard.speed);
        this.position = position;
        this.alignment = alignment;
    }

    // private PlacedCreature findTarget() {
    //     int targetAlignment = this.alignment == Utils.PLAYER? Utils.ENEMY: Utils.ENEMY;
    //     Team targetTeam = battleController.teams[targetAlignment];
    //     //attack directly infront in its own row
    //     if (targetTeam.placedCreatures[this.position] != null) {

    //     } 

    // }
    // TO DO: Add more functionality for checking abilities. 
    public void attack(int position) {
        // this.findTarget(); 
    }
}
