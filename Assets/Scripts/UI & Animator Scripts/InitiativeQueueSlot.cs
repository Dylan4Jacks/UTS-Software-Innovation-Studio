using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitiativeQueueSlot : MonoBehaviour
{
      public PlacedCreature creature;
      public SelectedCreatureBox selectedCreatureBox;
      public SpriteRenderer creatureSprite;
      public GameObject highlight;
      public int priority; 
    void Start() {
    }
    void Update() {
    }

    void OnMouseEnter() {
        if (creature == null) {
            return;
        }
        highlightSelf();

    }

    void OnMouseExit() {
        unHighlightSelf();
    }

    public void highlightSelf() {
        highlight.SetActive(true);
    }

    public void unHighlightSelf() {
        highlight.SetActive(false);
    }

    public void setCreature(PlacedCreature creature, GameObject creatureSelectionBox) {
        this.creature = creature;
        this.selectedCreatureBox = creatureSelectionBox.GetComponent<SelectedCreatureBox>();
        creatureSelectionBox.GetComponent<SelectedCreatureBox>().setInitiativeQueueSlot(this);
    }
}
