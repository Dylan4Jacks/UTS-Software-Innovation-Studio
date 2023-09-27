using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedCreatureBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InitiativeQueueSlot initiativeQueueSlot;
    public AnimationHandler animator;

    public void Start() {
        this.animator = gameObject.GetComponent<AnimationHandler>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered");
        highlightSelf();
        initiativeQueueSlot.highlightSelf();

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited");
        unHighlightSelf();
        initiativeQueueSlot.unHighlightSelf();

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
}
