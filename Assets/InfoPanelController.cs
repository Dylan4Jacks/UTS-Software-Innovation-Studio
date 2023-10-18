using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    public string currentState = "DEFAULT"; //DEFAULT, BASECARD, PLACEDCREATURE
    public static InfoPanelController instance; 
    public GameObject defaultView; 
    public GameObject creatureView;
    public TextMeshPro panelDescription; 
    public TextMeshPro creatureDescription;
    public TextMeshPro creatureName;
    public TextMeshPro creatureAttack;
    public TextMeshPro creatureSpeed;
    public TextMeshPro creatureHealth;
    public SpriteRenderer creatureSprite;
    public string defaultText = "Place your creatures!";
    public BaseCard baseCard; 
    public PlacedCreature placedCreature;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void viewPlacedCreature(PlacedCreature placedCreature) {
        defaultView.SetActive(false);
        creatureView.SetActive(true);

        this.placedCreature = placedCreature;
        this.baseCard = placedCreature.baseCard;

        creatureName.text = placedCreature.baseCard.cardName;
        creatureAttack.text = placedCreature.currentStrength.ToString();
        creatureSpeed.text = placedCreature.currentSpeed.ToString();
        creatureHealth.text = placedCreature.currentHealth.ToString();

        creatureSprite.sprite = placedCreature.baseCard.sprite;
    }

    public void viewBaseCard(BaseCard baseCard) {
        defaultView.SetActive(false);
        creatureView.SetActive(true);

        this.placedCreature = null;
        this.baseCard = baseCard;

        creatureName.text = baseCard.cardName;
        creatureAttack.text = baseCard.strength.ToString();
        creatureSpeed.text = baseCard.speed.ToString();
        creatureHealth.text = baseCard.health.ToString();

        creatureSprite.sprite = baseCard.sprite;
    }

    public void returnToDefault(string state) {
        this.placedCreature = null;
        this.baseCard = null;

        defaultView.SetActive(true);
        creatureView.SetActive(false);
        
        switch(state) {
            case "":  panelDescription.text = defaultText; break;
            case "EMPTY_SELECTION_BOX": panelDescription.text = "Select a creature card from your hand to place it in this empty slot." ;break;
            default: break; 
        }
    }
}
