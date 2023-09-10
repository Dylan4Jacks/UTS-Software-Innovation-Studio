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
    
    private TextMeshPro healthText;
    private TextMeshPro strengthText;
    private int alignment;
    private int position;
    private Team team;
    public bool isVictorious; 
    public PlacedCreature lanePartner; 
    // Awake is called when instantiated
    void Awake()
    {
        this.battleController = BattleController.instance;
        this.healthText = displayHealth.GetComponent<TextMeshPro>();
        this.strengthText = displayStrength.GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*************************************************
    * BASIC FUNCTIONS (setters, getters, etc.)
    *************************************************/
    public void setupCard(BaseCard baseCard, int alignment, int position) {
        this.baseStats = baseCard;
        setCurrentHealth(baseCard.health);
        setCurrentStrength(baseCard.strength);
        setCurrentSpeed(baseCard.speed);
        this.position = position;
        this.alignment = alignment;
        this.lanePartner = battleController.teams[alignment].placedCreatures[Utils.calculateLanePartner(this.position)];
        if (lanePartner != null) {
            lanePartner.setNewLanePartner(this);
        }
        this.team = BattleController.instance.teams[alignment];
    }
    public void setCurrentStrength(int value) {
        currentStrength = value;
        strengthText.text = value.ToString();
    }
    public void setCurrentHealth(int value) {
        currentHealth = value;
        healthText.text = value.ToString();
    }
    public void setCurrentSpeed(int value) {
        //TODO add display speed
        currentSpeed = value;
    }
    public bool hasLanePartner() {
        return lanePartner != null;
    }

    public void setNewLanePartner(PlacedCreature partner){
        this.lanePartner = partner;
    }
    /*************************************************
    * AUXILIARY FUNCTIONS (to derive things)
    *************************************************/
    public List<PlacedCreature> getLaneOpponents() {
        int lane = Utils.calculateLane(position);
        return team.getAdversary().getLaneCreatures(lane);
    }
    public bool canAttack() {
        // cards can only attack if: 
        // strength > 0
        // They are in the front row OR the creature infront has died
        // They have an attribute which lets them ignore the card infront

        bool canAttack = false; 
        if (position <= 2 || !hasLanePartner() || lanePartner.isSlain) {
            canAttack = true;
        } 
        return canAttack;
    }
    /*************************************************
    * BATTLE FUNCTIONS
    *************************************************/
    private PlacedCreature findTarget() {
        List<PlacedCreature> laneOpponents = this.getLaneOpponents();
        //attack directly infront 
        PlacedCreature target = laneOpponents[0];
        //attack directly infront in its own row
        if (target == null || target.isSlain) {
            target = laneOpponents[1];
            if (target == null || target.isSlain) {
                return null; 
            }
        }
        Debug.Log("Round " + battleController.currentRound.ToString() + ": " + this.baseStats.cardName + " is attacking " + target.baseStats.cardName);
        return target;
    }
    // TO DO: Add more functionality for checking abilities. 
    public void attack() {
        if (this.isSlain || !this.canAttack()) {
            return;
        }
        PlacedCreature target = this.findTarget();
        if (target == null) {
            return;
        }
        
        attack(target);
    }

    public void attack(PlacedCreature target) {
        target.beAttacked(this);
        this.checkDeath(target); //check death after damage taken from retaliation
    }

    public void beAttacked(PlacedCreature attacker) {
        //check abilities to see if anything activates upon being attacked
        //then take damage
        //then retaliate
        // Debug.Log(attacker.baseStats.cardName + " is attacking " + this.baseStats.cardName);
        setCurrentHealth(currentHealth - attacker.currentStrength);
        retaliate(attacker);
        this.checkDeath(attacker); 
    }

    public void retaliate(PlacedCreature attacker) {
        // Debug.Log(this.baseStats.cardName + " is retaliating against" + attacker.baseStats.cardName);
        setCurrentHealth(attacker.currentHealth - this.currentStrength);
    }

    public void checkDeath(PlacedCreature killer) {
        if (this.currentHealth <= 0) {
            this.isSlain = true;
            Debug.Log(this.baseStats.cardName + " has been slain by " + killer.baseStats.cardName);
        }
    }

    public void perish() {
        this.isSlain = true;
    }
    
}
