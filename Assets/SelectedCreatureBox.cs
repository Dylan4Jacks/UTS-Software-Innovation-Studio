using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectedCreatureBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InitiativeQueueSlot initiativeQueueSlot;
    public AnimationHandler animator;
    public int team; 
    public int slotPosition;
    public Battlefield battlefield;

    public void Start() {
        this.animator = gameObject.GetComponent<AnimationHandler>();
    }

    public void onHover () {
        if (PlayerHand.Instance.hasHoveredCard) {return;}
        highlightSelf();
        PlacedCreature creature = getCreature();
        if (creature == null) {
            InfoPanelController.instance.returnToDefault("EMPTY_SELECTION_BOX");
            initiativeQueueSlot = null;
        } else {
            InfoPanelController.instance.viewPlacedCreature(creature);
            int queueIndex = BattleController.instance.initiativeQueue.FindIndex(a => a.Equals(creature));
            initiativeQueueSlot = queueIndex > 5? null : InitiativeQueueUI.instance.slots[queueIndex]; 
        }
        if (initiativeQueueSlot != null) { initiativeQueueSlot.highlightSelf(); }
    }

    public void onUnhover() {
        unHighlightSelf();
        if (!PlayerHand.Instance.hasHoveredCard){
            InfoPanelController.instance.returnToDefault("");
        }

        if (initiativeQueueSlot == null) {
            return;
        }
        initiativeQueueSlot.unHighlightSelf();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        battlefield.currentSelection = this;
        onHover();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       battlefield.currentSelection = null;
       onUnhover();
    }

    public void OnMouseDown() {
        this.placeCreature();
    }

    public void highlightSelf() {
        animator.changeAnimationState("SelectedContainer");
    }

    public void unHighlightSelf() {
        animator.changeAnimationState("Opacity_0");
    }
    public void setInitiativeQueueSlot(InitiativeQueueSlot initiativeQueueSlot) {
        this.initiativeQueueSlot = initiativeQueueSlot;
    }

    public void emptySlot() {
        this.initiativeQueueSlot = null;
    }

    public void placeCreature() {
        if (PlayerHand.Instance.selectedCard != null) {
            PlayerHand.Instance.placeCreature(this.team, this.slotPosition);
        }
    }

    public bool hasCreature() {
        return BattleController.instance.teams[team].placedCreatures[slotPosition] != null;
    }

    public PlacedCreature getCreature() {
        return BattleController.instance.teams[team].placedCreatures[slotPosition];
    }
}
