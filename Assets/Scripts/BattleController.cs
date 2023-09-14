using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
public class BattleController : MonoBehaviour
{
    // Related to battle
    public bool enableRoundBreaks = false;
    public int currentRound = 0;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();
    [SerializeField] public List<int> laneVictors = new List<int>(new int[3]);

    // For referencing
    public static BattleController instance;
    public RoundCounter roundCounter;
    public GameObject creaturePrefab;
    public List<Team> teams = new List<Team>(); 
    


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
        generateNewTestTeam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /********************************************
    * publicly visible functions
    *********************************************/
    public void generateNewTestTeam() {
        resetBattle();
        for (int i = 0; i < 3; i++) {
            laneVictors[i] = -1; //temporary for debugging
        } 
        for (int i = 0; i < 6; i++) {
            teams[Utils.ENEMY].placeCreature(i, new BaseCard("Enemy_"+i.ToString(), Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
            teams[Utils.PLAYER].placeCreature(i, new BaseCard("Player_"+i.ToString(), Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
        }
        fillInitiativeQueue();
    }

    public void startBattle() {
        StartCoroutine(runBattle());
    }
    public void toggleRoundBreaks() {
        enableRoundBreaks = !enableRoundBreaks;
    }
    /********************************************
    * Battle logic
    *********************************************/
    private IEnumerator runBattle () {
        do {
            yield return runRound();
            if (enableRoundBreaks) {
                roundCounter.setText("Round " + currentRound + " End");
                break;
            };
        } while (hasUnresolvedLanes());
    }
    private IEnumerator runRound() {
        this.currentRound += 1; 
        roundCounter.setText("Round " + this.currentRound.ToString());
        fillInitiativeQueue();
        //start looping through the initiative queue to do battle
        foreach (PlacedCreature creature in initiativeQueue) {
            //do this thing here where it waits for the previous one to finish.
            Debug.Log(Utils.roundTemplate() + creature.baseStats.cardName + " to act");
            yield return StartCoroutine(creature.attack());
            Debug.Log(Utils.roundTemplate() + creature.baseStats.cardName + " turn finished");
        }
        resolveLaneVictories();
        if (!hasUnresolvedLanes()) {
            Debug.Log("battle's over");
            handleBattleEnd();
        }
    }

    private void fillInitiativeQueue() {
        initiativeQueue.Clear();
        foreach (Team team in this.teams) {
            foreach (PlacedCreature creature in team.placedCreatures) {
                if (!creature.isSlain && !creature.isVictorious) {
                    initiativeQueue.Add(creature);
                }
            }
        }
        sortInitiativeQueue();
    }

    private void sortInitiativeQueue() {
        initiativeQueue.Sort(Utils.ComparePlacedCreaturesBySpeed);
        InitiativeQueueUI.instance.handleNewQueue(initiativeQueue, this);
    }

    /********************************************
    * battle outcomes
    *********************************************/
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

     //TO DO: flesh this out
    private void handleBattleEnd() {
        roundCounter.setText(Utils.alignmentString(determineWinner()) + " wins!");
        Debug.Log("Winner: " + Utils.alignmentString(determineWinner()));
    }

    /********************************************
    * Misc internal
    *********************************************/
    private void resetBattle() {
        Utils.ClearConsole();
        this.currentRound = 0;
        roundCounter.setText("PREPARATION");
    }
}
