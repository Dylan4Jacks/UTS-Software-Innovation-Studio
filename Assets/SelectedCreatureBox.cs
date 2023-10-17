using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedCreatureBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InitiativeQueueSlot initiativeQueueSlot;
    public AnimationHandler animator;
    public int team; 
    public int slotPosition;
    private BattleController battleController;

    public void Start() {
        this.battleController = BattleController.instance;
        this.animator = gameObject.GetComponent<AnimationHandler>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightSelf();
        if (initiativeQueueSlot == null) {return;}
        initiativeQueueSlot.highlightSelf();

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        unHighlightSelf();
        if (initiativeQueueSlot == null) {return;}
        initiativeQueueSlot.unHighlightSelf();

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
}
