using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitiativeQueueSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
      public PlacedCreature creature;
      public AnimationHandler animator;
      public SelectedCreatureBox selectedCreatureBox;
      public int priority; 

    // public void 
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
