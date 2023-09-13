using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class AnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Animator animator;
    [SerializeField] protected string currentState;

    protected void changeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    protected IEnumerator waitForAnimationToFinish() {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null; 
        }
    }
    protected IEnumerator waitForAnimatorStateChange() {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime != 0) {
        
            yield return null;
        }
    }
}
