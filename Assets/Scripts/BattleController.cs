using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    // Related to battle
    public bool enableRoundBreaks = true;
    public int currentRound = 0;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();
    [SerializeField] public List<int> laneVictors = new List<int>(new int[3]);
    string battleState = "PREPARATION";
    public InfoPanelButton battleButton;

    // For referencing
    public static BattleController instance;
    public RoundCounter roundCounter;
    public GameObject creaturePrefab;
    public List<Team> teams = new List<Team>(); 
    public InitiativeQueueUI initiativeQueueUI;
    public InfoPanelController infoPanel;
    
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
        setupReferences();
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
        for (int i = 0; i < 3; i++) {
            laneVictors[i] = -1; //temporary for debugging
        } 
        for (int i = 0; i < 6; i++) {
            teams[Utils.ENEMY].placeCreature(i, SingleCharacter.Instance.enemyCards[i]);
        }
    }

    public void startBattle() {
        // activateInitiationAbilities(); 

        PlayerHand playerHand = PlayerHand.Instance;
        while(playerHand.cardsInHand.Count > 0) {
            CardInHand card = playerHand.cardsInHand[0];
            playerHand.cardsInHand.Remove(card);
            Destroy(card.gameObject);
        }
        StartCoroutine(runBattle());
    }
    public void toggleRoundBreaks() {
        enableRoundBreaks = !enableRoundBreaks;
    }
    /********************************************
    * Battle logic
    *********************************************/
    private void activateInitiationAbilities ()
    {
        foreach (PlacedCreature creature in initiativeQueue)
        {
           creature.triggerInitialAbilities();
        }
    }
    private IEnumerator runBattle () {
        switch (battleState) {
            case "PREPARATION": yield return StateBanner.instance.summonBanner("BATTLE START!", 0.5f); break;
            default: break;
        }
        do {
            yield return runRound();
            if (enableRoundBreaks && battleState != "BATTLE_END") {
                changeBattleState("ROUND_BREAK");
                break;
            };
        } while (hasUnresolvedLanes());
    }
    private IEnumerator runRound() {
        changeBattleState("BATTLING");
        battleButton.setWaitForRound();
        this.currentRound += 1; 
        roundCounter.setText("Round " + this.currentRound.ToString());
        // fillInitiativeQueue();
        //start looping through the initiative queue to do battle
        foreach (PlacedCreature creature in initiativeQueue) {
            //do this thing here where it waits for the previous one to finish.
            Debug.Log(Utils.roundTemplate() + creature.baseCard.cardName + " to act");
            yield return StartCoroutine(creature.attack());
            Debug.Log(Utils.roundTemplate() + creature.baseCard.cardName + " turn finished");
        }
        resolveLaneVictories();
        if (!hasUnresolvedLanes()) {
            Debug.Log("battle's over");
            handleBattleEnd();
        }
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

    private void handleBattleEnd() {
        StartCoroutine(StateBanner.instance.summonBanner(Utils.alignmentString(determineWinner()).ToUpper() + " WINS",0.5f));
        roundCounter.setText(Utils.alignmentString(determineWinner()) + " wins!");
        Debug.Log("Winner: " + Utils.alignmentString(determineWinner()));
        changeBattleState("BATTLE_END");
        battleButton.setGameOver();
    }

    /*******************************************
    * Initiative Queue Stuff
    ********************************************/
    public void insertInInitiativeQueue(PlacedCreature creature) {
        if (initiativeQueue.Count == 0) {
            initiativeQueue.Add(creature);
            //do initiative queue UI thing
        } else {
            for(int i = 0; i < initiativeQueue.Count; i++) {
                if (creature.currentSpeed > initiativeQueue[i].currentSpeed) {
                    initiativeQueue.Insert(i, creature);
                    initiativeQueueUI.updateUI();
                    return;
                }
            }
            initiativeQueue.Add(creature);
        }
        
    }

    public void removeFromInitiativeQueue(PlacedCreature creature) {
        initiativeQueue.Remove(creature);
        //do initiative queue UI thing
    }

    /********************************************
    * Misc internal
    *********************************************/
    private void resetBattle() {
        generateNewTestTeam();
        
        this.currentRound = 0;
        changeBattleState("PREPARATION");
    }

    private void changeBattleState(string newState) {
        infoPanel.changeBattleState(newState);
        if (battleState == "BATTLE_END" && newState != "PREPARATION") {
            return;
        }
        switch(newState) {
            case "PREPARATION": 
                roundCounter.setText("PREPARATION");
                battleButton.setInitial();
                break;
            case "BATTLING":
                battleButton.setWaitForRound();
                break;
            case "BATTLE_END":
                battleButton.setGameOver();
                break;
            case "ROUND_BREAK":
                battleButton.setNextRound();
                break;
            default: break;
        }
        battleState = newState;
    }

    private void setupReferences() {
        initiativeQueueUI.battleController = this;
    }
}
