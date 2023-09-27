using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectAnimationHandler : AnimationHandler
{
    PlacedCreature sourceCreature;
    public IEnumerator deathExplosion (PlacedCreature sourceCreature) {
        this.sourceCreature = sourceCreature;
        changeAnimationState("Explode");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
    }

    public IEnumerator sourceCreatureDie() {
        yield return sourceCreature.creatureAnimator.creatureDead();
    }
}
