using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationHandler : AnimationHandler
{   
    public PlacedCreature self;
    public GameObject effectsHandler;
    private PlacedCreature target; 
    public IEnumerator basicAttack(int alignment, PlacedCreature target) {
        this.target = target;
        changeAnimationState("creaturePreparingAttack");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
        changeAnimationState(alignment == Utils.PLAYER? "creatureBasicAttackPlayer" : "creatureBasicAttackEnemy");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
        changeAnimationState("Idle");
    } 

    public IEnumerator perish() {
        GameObject deathEffect = Instantiate(
            effectsHandler, 
            self.gameObject.GetComponent<Transform>().position, 
            Quaternion.identity
            );
        deathEffect.transform.parent = self.gameObject.transform;
        yield return StartCoroutine(deathEffect.GetComponent<VisualEffectAnimationHandler>().deathExplosion(self));
    }

    public IEnumerator creatureDead() {
        changeAnimationState("creatureDead");
        yield return waitForAnimatorStateChange();
    }
}
