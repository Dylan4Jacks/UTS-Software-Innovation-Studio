using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingleCharacter : MonoBehaviour
{
    public static SingleCharacter Instance;
    public List<BaseCard> cards;
    public string CharacterDescription;

    private void Awake()
    {
        // Ensure that only one instance of this class exists.
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the object persists across scenes
        }
        else {
            Destroy(gameObject); // Destroy any additional instances
        }
    }
}
