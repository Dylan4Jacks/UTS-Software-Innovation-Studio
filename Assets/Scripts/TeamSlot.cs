using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamSlot : MonoBehaviour
{
    #nullable enable
    private PlacedCreature? placedCreature;
    #nullable disable
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool hasCreature() {
        return (placedCreature != null);
    }
}


