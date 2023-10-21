using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class InfoPanelController : MonoBehaviour
{
    public string currentState = "DEFAULT"; //DEFAULT, BASECARD, PLACEDCREATURE
    public static InfoPanelController instance; 
    public GameObject defaultView, creatureView, promptReminderView, errorView;
    public TextMeshPro panelDescription; 
    public TextMeshPro creatureDescription, creatureName, creatureAttack, creatureSpeed, creatureHealth,
        movePriority;
    public SpriteRenderer creatureSprite;
    public string defaultText = "Place your creatures!";
    public BaseCard baseCard; 
    public PlacedCreature placedCreature;
    public TextMeshPro errorReason, errorTitle;

    
    //LIST STUFF
    public List<string> possibleErrorTitles;
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
        creatureDescription.margin = new Vector4(-52.5f, 0f, -72.8f, -51f);
        movePriority.gameObject.SetActive(true);
        if (placedCreature.killer != null) {
            movePriority.text = "Slain by " + placedCreature.killer.baseCard.cardName;
        } else {
            int initiativeQueuePos = BattleController.instance.initiativeQueue.FindIndex(a => a.Equals(placedCreature)) + 1;
            movePriority.text = "Moves " + initiativeQueuePos.ToString() + 
            (initiativeQueuePos == 1? "st" : initiativeQueuePos == 2? "nd" : initiativeQueuePos == 3? "rd" : "th");
        }

        defaultView.SetActive(false);
        creatureView.SetActive(true);
        promptReminderView.SetActive(false);
        errorView.SetActive(false);

        this.placedCreature = placedCreature;
        this.baseCard = placedCreature.baseCard;

        creatureName.text = placedCreature.baseCard.cardName;
        creatureAttack.text = placedCreature.currentStrength.ToString() + "/" + placedCreature.baseCard.strength;
        creatureSpeed.text = placedCreature.currentSpeed.ToString();
        creatureHealth.text = placedCreature.currentHealth.ToString();
        creatureDescription.text = baseCard.description;


        creatureSprite.sprite = placedCreature.baseCard.sprite;
    }

    public void viewBaseCard(BaseCard baseCard) {
        creatureDescription.margin = new Vector4(-52.5f, 0f, -72.8f, -51f);
        movePriority.gameObject.SetActive(false);

        defaultView.SetActive(false);
        creatureView.SetActive(true);
        promptReminderView.SetActive(false);
        errorView.SetActive(false);

        this.placedCreature = null;
        this.baseCard = baseCard;

        creatureName.text = baseCard.cardName;
        creatureAttack.text = baseCard.strength.ToString();
        creatureSpeed.text = baseCard.speed.ToString();
        creatureHealth.text = baseCard.health.ToString();
        creatureDescription.text = baseCard.description;

        creatureSprite.sprite = baseCard.sprite;
    }

    public void returnToDefault(string state) {
        this.placedCreature = null;
        this.baseCard = null;

        defaultView.SetActive(true);
        creatureView.SetActive(false);
        promptReminderView.SetActive(false);
        errorView.SetActive(false);
        
        switch(state) {
            case "":  panelDescription.text = defaultText; break;
            case "EMPTY_SELECTION_BOX": 
                if (PlayerHand.Instance.selectedCard == null) {
                    panelDescription.text = "Select a creature card from your hand to place it in this empty slot." ;
                } else {
                    panelDescription.text = "Click to play '" + PlayerHand.Instance.selectedCard.baseCard.cardName + "' here";
                }
                break;
            default: break; 
        }
    }

    public void setPromptReminderView() {
        defaultView.SetActive(false);
        creatureView.SetActive(false);
        promptReminderView.SetActive(true);      
        errorView.SetActive(false);  
    }

    public void changeBattleState(string newState) {
        switch(newState) {
            case "PREPARATION": 
            defaultText = "Place your creatures!";
                break;
            case "BATTLING":
            defaultText = "Round " + (BattleController.instance.currentRound + 1) + 
                " in progress...";
                break;
            case "BATTLE_END":
            defaultText = "Click the back button in the top right to play again!";
                break;
            case "ROUND_BREAK":
            defaultText = "Press the 'Next Round' button to continue";
                break;
            default: break;
        }
        panelDescription.text = defaultText;
    }

    public void setErrorView(string errorReason) {
        defaultView.SetActive(false);
        creatureView.SetActive(false);
        promptReminderView.SetActive(false);
        errorView.SetActive(true);
        this.errorReason.text = errorReason;
        errorTitle.text = possibleErrorTitles[UnityEngine.Random.Range(0, possibleErrorTitles.Count)];
    }
}
