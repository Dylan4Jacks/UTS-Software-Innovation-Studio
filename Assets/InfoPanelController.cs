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
    public string defaultText = "Place your creatures!";
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
    }

    public void viewBaseCard(BaseCard baseCard) {
        defaultView.SetActive(false);
        creatureView.SetActive(true);
    }

    public void returnToDefault(string state) {
        defaultView.SetActive(true);
        creatureView.SetActive(false);
        
        switch(state) {
            case "":  panelDescription.text = defaultText; break;
            case "EMPTY_SELECTION_BOX": panelDescription.text = "Select a creature card from your hand to place it in this empty slot." ;break;
            default: break; 
        }
    }
}
