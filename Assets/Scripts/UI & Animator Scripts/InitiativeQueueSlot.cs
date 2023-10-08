using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitiativeQueueSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
      public PlacedCreature creature;
      public AnimationHandler animator;
      public SelectedCreatureBox selectedCreatureBox;
      public Sprite creatureSprite;
      public Sprite deathSprite;
      public int priority; 
      private UnityEngine.UI.Image currentImage;
    void Start() {
        currentImage = gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
    }
    void Update() {
        if (currentImage.sprite == creatureSprite && creature.isSlain) {
            currentImage.sprite = deathSprite;
        }
    }
     public void OnPointerEnter(PointerEventData eventData)
    {
        highlightSelf();
        selectedCreatureBox.highlightSelf();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        unHighlightSelf();
        selectedCreatureBox.unHighlightSelf();
    }

    public void highlightSelf() {
        animator.changeAnimationState("Highlighted");
    }

    public void unHighlightSelf() {
        animator.changeAnimationState("Normal");
    }
    
    public void setupSlot(PlacedCreature creature, GameObject creatureSelectionBox) {
        this.creature = creature;
        this.selectedCreatureBox = creatureSelectionBox.GetComponent<SelectedCreatureBox>();
        creatureSelectionBox.GetComponent<SelectedCreatureBox>().setInitiativeQueueSlot(this);
    }
}
