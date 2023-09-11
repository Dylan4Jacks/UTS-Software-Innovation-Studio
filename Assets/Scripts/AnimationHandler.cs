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
    void Update()
    {
 
    }
    public void changeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
