using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeResources : MonoBehaviour
{
    public static RuntimeResources Instance;
    public Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    void Awake() 
    {
        if (Instance != null && Instance != this) {
                Destroy(this);
            }
        else {
            Instance = this;
        }

        initialiseSprites();
    }

    private void initialiseSprites() {
        spriteDictionary.Add("wug", Resources.Load<Sprite>("Sprites/Wug"));
        spriteDictionary.Add("humanoid", Resources.Load<Sprite>("Sprites/humanoid"));
        spriteDictionary.Add("beast", Resources.Load<Sprite>("Sprites/beast"));
        spriteDictionary.Add("furniture", Resources.Load<Sprite>("Sprites/furniture"));
    }
}
