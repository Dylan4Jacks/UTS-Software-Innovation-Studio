using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleController : MonoBehaviour
{
    public List<Card> initiativeQueue = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        testBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void testBattle() {
        Debug.Log("Test battle starting...");
        Debug.Log("Assigning to player team front row left side");
    }

    //TODO make this an onupdate thing
    private void updateInitiativeQueue() {
        
    }
}
