using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {

    //array positions 0 - 5 in order
    // 0.front row left, 1.front row middle, 2.front row right
    // 3.back row left, 4.back row middle, 5.back row right
    public List<Card> teamPositions = new List<Card>() {null, null, null, null, null, null};

    public Team(
        
        ) {
            //all teams initialised empty
        }

    public void assignTeamSlot(int position, Card card) {
        if (position <= 5 && position >= 0) {
            teamPositions[position] = card;
        }
    }
}

public class BattleController : MonoBehaviour
{
    public Team playerTeam = new Team();
    public Team enemyTeam = new Team();
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
        playerTeam.assignTeamSlot(0, new Card("Test Creature 1", 10, 3, 30));
        enemyTeam.assignTeamSlot(0, new Card("Enemy Creature", 5, 10, 20));
    }

    //TODO make this an onupdate thing
    private void updateInitiativeQueue() {
        
    }
}
