using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitiativeQueueSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
      public PlacedCreature creature;
      public AnimationHandler animator;
      public GameObject creatureSelectionBox;
      public int priority; 

     public void OnPointerEnter(PointerEventData eventData)
    {
        animator.changeAnimationState("Highlighted");
        creatureSelectionBox.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.changeAnimationState("Normal");
        creatureSelectionBox.SetActive(false);
    }
}
