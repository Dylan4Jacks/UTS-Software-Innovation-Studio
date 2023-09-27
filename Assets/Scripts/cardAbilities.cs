using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbilities : MonoBehaviour
{
    string[] abilities = {
        "heal",
        "areaHeal",
        "areaDamage",
        "strengthIncrease",
        "sheildIncrease",
        "speedIncrease",
        "strengthDecrease",
        "sheildDecrease",
        "speedDecrease",
    };

    // Heal Ability
        // Range: Need to determine the range of base stats first
        // Targets: One adjacent card - front, back, left, right (can add diagonals if appropriate)

    //Area Heal Ability
        // Range: Need to determine the range of base stats first, will be lower than Heal Ability
        // Targets: 

    // Area Damage Ability
        // Range: Need to determine range of base stats first
        // Targets: One or more targets adjacent to primary target of this card (whatever card this card normally attacks, the cards to its left, right and behind/infront are the targets of this affect)

    // Strength Increase
        // Permanent one off increase of the strength of a single card that is adjacent to the card this ability is on (primarliy behind/to the side rather than in front

    // Sheild Increase
        // Permanent one off increase of the sheild of a single card that is adjacent to the card this ability is on (primarliy behind/to the side rather than in front

    // Speed Increase
        // Permanent one off increase of the speed of a single card that is adjacent to the card this ability is on (primarliy behind/to the side rather than in front

    // Strength Decrease
        // Permanent one off decrease of the strength of a single card that is opposite this card (target, or adjacent to target)

    // Sheild Decrease
        // Permanent one off decrease of the sheild of a single card that is opposite this card (target, or adjacent to target)

     // Speed Decrease
        // Permanent one off decrease of the speed of a single card that is opposite this card (target, or adjacent to target)

}
