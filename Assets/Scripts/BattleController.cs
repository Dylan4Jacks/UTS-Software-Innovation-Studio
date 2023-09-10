using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    public int currentRound = 0;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();
    public GameObject roundCounter;
    public GameObject creaturePrefab;
    public List<Team> teams = new List<Team>(); 

    // [SerializeField] private GameObject enemyTeam;
    // [SerializeField] private GameObject playerTeam;

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
        currentRound += 1; 
        roundCounter.GetComponent<TextMeshPro>().text = "Round " + currentRound.ToString();

        //populate and sort the initiative queue
        initiativeQueue.AddRange(teams[Utils.PLAYER].GetComponentsInChildren<PlacedCreature>());
        initiativeQueue.AddRange(teams[Utils.ENEMY].GetComponentsInChildren<PlacedCreature>());
        sortInitiativeQueue();
        
        //start looping through the initiative queue to do battle
        for (int i = 0; i < initiativeQueue.Count; i++) {

        }
    }
    private void testBattle() {
        for (int i = 0; i < 6; i++) {
            teams[Utils.ENEMY].placeCreature(i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
            teams[Utils.PLAYER].placeCreature(i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
        }
        roundStart();
    }
}
