using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacedCreature : MonoBehaviour
{
    public BaseCard baseStats;
    public int currentHealth;
    public int currentStrength;
    public int currentSpeed; 
    public GameObject displayHealth;
    public GameObject displayStrength;
    
    // Awake is called when instantiated
    void Awake()
    {

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

    public void setupCard(BaseCard baseCard) {
        this.baseStats = baseCard;
        setCurrentHealth(baseCard.health);
        setCurrentStrength(baseCard.strength);
        setCurrentSpeed(baseCard.speed);
    }
}
