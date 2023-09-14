using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AnimationHandler animator;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.changeAnimationState("NavArrowHighlighted");

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.changeAnimationState("NavArrowIdle");

    }
}
