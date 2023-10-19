using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro originalPromptText;
    void Start()
    {
        if (SingleCharacter.Instance != null && SingleCharacter.Instance.CharacterDescription != null) {
            originalPromptText.text = SingleCharacter.Instance.CharacterDescription;
        } else {
            originalPromptText.text = "Oh boy, I hope you're running this game in debug or dev mode, buddy, because the text here is lookin pretty lorem ipsum to me.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
