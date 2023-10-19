using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeResources : MonoBehaviour
{
    public static RuntimeResources Instance;
    public Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    public Texture2D cursorTexture;
    void Awake() 
    {
        if (Instance != null && Instance != this) {
                Destroy(this);
            }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        initialiseSprites();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void initialiseSprites() {
        spriteDictionary.Add("wug", Resources.Load<Sprite>("Sprites/Wug"));
        spriteDictionary.Add("humanoid", Resources.Load<Sprite>("Sprites/humanoid"));
        spriteDictionary.Add("beast", Resources.Load<Sprite>("Sprites/beast"));
        spriteDictionary.Add("furniture", Resources.Load<Sprite>("Sprites/furniture"));
        spriteDictionary.Add("ghost", Resources.Load<Sprite>("Sprites/ghost"));
        spriteDictionary.Add("car", Resources.Load<Sprite>("Sprites/car"));
        spriteDictionary.Add("alien", Resources.Load<Sprite>("Sprites/alien"));
        spriteDictionary.Add("creature", Resources.Load<Sprite>("Sprite/creature"));
        spriteDictionary.Add("whale", Resources.Load<Sprite>("Sprites/whale"));
        spriteDictionary.Add("robot", Resources.Load<Sprite>("Sprites/robot"));
        spriteDictionary.Add("unknown", Resources.Load<Sprite>("Sprite/unknown"));
    }
}
