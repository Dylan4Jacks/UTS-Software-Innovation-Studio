using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGameAudio : MonoBehaviour
{
    static SingleGameAudio instance;

    void Awake()
    {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
