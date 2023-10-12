using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CreatureAnimationHandler : AnimationHandler
{   
    public PlacedCreature self;
    public GameObject effectsHandler;
    private PlacedCreature target; 
    public GameObject deadCreatureObject;
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
        changeAnimationState("Creature_dying");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
        deadCreatureObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
