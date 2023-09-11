using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    public int currentRound = 0;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();
    public GameObject roundCounter;
    public GameObject creaturePrefab;
    public List<Team> teams = new List<Team>(); 
    [SerializeField] public List<int> laneVictors = new List<int>(new int[3]);

    void Awake() {
        teams[Utils.ENEMY].alignment = Utils.ENEMY;
        teams[Utils.PLAYER].alignment = Utils.PLAYER;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
        testBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO make this an onupdate thing
    private void updateInitiativeQueue() {
        
    }

    private void sortInitiativeQueue() {
        initiativeQueue.Sort(Utils.ComparePlacedCreaturesBySpeed);
    }
    private void roundStart() {
        this.currentRound += 1; 

        //populate and sort the initiative queue
        foreach (Team team in this.teams) {
            foreach (PlacedCreature creature in team.placedCreatures) {
                if (!creature.isSlain && !creature.isVictorious) {
                    initiativeQueue.Add(creature);
                }
            }
        }
        sortInitiativeQueue();
        //start looping through the initiative queue to do battle
        foreach (PlacedCreature creature in initiativeQueue) {
            creature.attack();
        }
    }
    private void testBattle() {
        for (int i = 0; i < 3; i++) {
            laneVictors[i] = -1; //temporary for debugging
        } 
        for (int i = 0; i < 6; i++) {
            teams[Utils.ENEMY].placeCreature(i, new BaseCard("Enemy_"+i.ToString(), Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
            teams[Utils.PLAYER].placeCreature(i, new BaseCard("Player_"+i.ToString(), Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
        }
        do {
            roundStart();
            resolveLaneVictories();
            if (this.currentRound == 50) {
                Debug.Log("Round has exceeded 50. Something is very wrong.");
                break;
            }
            initiativeQueue.Clear();
        } while (hasUnresolvedLanes());
        if (!hasUnresolvedLanes()) {
            Debug.Log("Winner: " + determineWinner().ToString());
        }
    }

    private bool hasUnresolvedLanes() {
        bool hasUnresolvedLanes = false;
        for (int lane = 0; lane < laneVictors.Count; lane++) {
            if (laneVictors[lane] == -1) {
                hasUnresolvedLanes = true;
            }
        }
        return hasUnresolvedLanes;
    }
    private void resolveLaneVictories() {
        for (int lane = 0; lane < laneVictors.Count; lane++) {
            if (laneVictors[lane] == -1) {
                if (teams[Utils.ENEMY].isLaneDefeated(lane) && teams[Utils.PLAYER].isLaneDefeated(lane)) {
                    Debug.Log("Lane " + lane.ToString() + " is a draw.");
                    laneVictors[lane] = Utils.NO_ALIGNMENT; //this is a draw
                } else if (teams[Utils.ENEMY].isLaneDefeated(lane)) {
                    Debug.Log("Player has won Lane " + lane.ToString());
                    teams[Utils.ENEMY].getAdversary().setVictoriousLane(lane);
                } else if (teams[Utils.PLAYER].isLaneDefeated(lane)) {
                    Debug.Log("Enemy has won Lane " + lane.ToString());
                    teams[Utils.PLAYER].getAdversary().setVictoriousLane(lane);
                }
            }
        }
    }

    private int determineWinner() {
        int enemyWinCount = 0;
        int playerWinCount = 0;
        int drawCount = 0;

        for(int lane = 0; lane < laneVictors.Count; lane++) {
            if (laneVictors[lane] == Utils.PLAYER) { playerWinCount += 1; }
            if (laneVictors[lane] == Utils.ENEMY) { enemyWinCount += 1; }
            if (laneVictors[lane] == Utils.NO_ALIGNMENT) { drawCount += 1; }
        }
        if (enemyWinCount > playerWinCount && enemyWinCount > drawCount) {return Utils.ENEMY;}
        else if (playerWinCount > enemyWinCount && playerWinCount > drawCount) {return Utils.PLAYER;}
        else {return Utils.NO_ALIGNMENT;}
    }
}
