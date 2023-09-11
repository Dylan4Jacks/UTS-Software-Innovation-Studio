using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationHandler : AnimationHandler
{   
    PlacedCreature target;
    public IEnumerator basicAttack(int alignment, PlacedCreature target) {
        this.target = target;
        changeAnimationState(alignment == Utils.PLAYER? "creatureBasicAttackPlayer" : "creatureBasicAttackEnemy");
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null; 
        }
        changeAnimationState("Idle");
    } 
}
