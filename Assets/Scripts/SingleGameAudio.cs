using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGameAudio : MonoBehaviour
{
    static SingleGameAudio instance;

    private void Awake()
    {
        if(instance != null) {
            Destroy(gameObject);
        }
    }
}
