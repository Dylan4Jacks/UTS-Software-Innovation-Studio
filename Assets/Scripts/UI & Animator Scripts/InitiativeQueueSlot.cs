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
      public GameObject deathSprite;
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
        if (creature != null) {
            selectedCreatureBox.highlightSelf();
            highlight.SetActive(true);
        }
    }

    public void unHighlightSelf() {
        if (creature != null) {
            selectedCreatureBox.unHighlightSelf();
            highlight.SetActive(false);        
        }
    }

    public void setCreature(PlacedCreature creature) {
        this.creature = creature;
        if (creature.isSlain) {
            creatureSprite.gameObject.SetActive(false);
            deathSprite.SetActive(true);
        } else {
            deathSprite.SetActive(false);
            creatureSprite.gameObject.SetActive(true);
            creatureSprite.sprite = creature.baseCard.sprite;
        }
        
        Team team = BattleController.instance.teams[creature.alignment];
        GameObject relevantContainer = team.creatureContainers[Utils.calculateLane(creature.position)];
        GameObject creatureSelectionBox = Utils.getChildren(relevantContainer)[creature.position > 2? 1 : 0];

        this.selectedCreatureBox = creatureSelectionBox.GetComponent<SelectedCreatureBox>();
        creatureSelectionBox.GetComponent<SelectedCreatureBox>().setInitiativeQueueSlot(this);
    }
    public void empty() {
        if (selectedCreatureBox != null) { selectedCreatureBox.emptySlot(); }
        this.creature = null;
        this.selectedCreatureBox = null;
    }
}
