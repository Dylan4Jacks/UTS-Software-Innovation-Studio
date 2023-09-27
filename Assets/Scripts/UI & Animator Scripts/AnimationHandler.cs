using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class AnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Animator animator;
    [SerializeField] public string currentState;

    public void Start() {
        this.animator = gameObject.GetComponent<Animator>();
    }
    public void changeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public IEnumerator waitForAnimationToFinish() {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null; 
        }
    }
    public IEnumerator waitForAnimatorStateChange() {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime != 0) {
        
            yield return null;
        }
    }
}
