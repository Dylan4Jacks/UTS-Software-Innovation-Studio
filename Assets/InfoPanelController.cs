using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    public string currentState = "DEFAULT"; //DEFAULT, BASECARD, PLACEDCREATURE
    public static InfoPanelController instance; 
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

    }

    public void viewBaseCard(BaseCard baseCard) {

    }

    public void returnToDefault() {
        
    }
}
