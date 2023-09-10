using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleController : MonoBehaviour
{
    public int currentRound = 0;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();
    public GameObject roundCounter;
    public GameObject enemyTeam;
    public GameObject playerTeam;
    public List<GameObject> creatureContainers; 
    public GameObject creaturePrefab;

    void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        testBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO make this an onupdate thing
    private void updateInitiativeQueue() {
        
    }
    public void placeCreature(int alignment, int position, BaseCard baseCard) {
        GameObject relevantContainer = creatureContainers[Utils.calculateRelevantContainer(alignment, position)];
        Transform containerTransform = relevantContainer.GetComponent<Transform>();
        GameObject placedCreature = Instantiate(creaturePrefab, containerTransform.position, Quaternion.identity);
        Transform placedCreatureTransform =  placedCreature.GetComponent<Transform>();

        placedCreatureTransform.position += Vector3.back * 7; 
        placedCreatureTransform.position += Vector3.left * 3;
        placedCreatureTransform.position += ((position > 2)? Vector3.down : Vector3.up) * 48;

        placedCreature.GetComponent<PlacedCreature>().setupCard(baseCard);
        placedCreature.transform.parent = alignment == Utils.PLAYER? 
            playerTeam.GetComponent<Team>().teamSlots[position].transform : 
            enemyTeam.GetComponent<Team>().teamSlots[position].transform;
    } 

    private void sortInitiativeQueue() {
        initiativeQueue.Sort(Utils.ComparePlacedCreaturesBySpeed);
    }
    private void roundStart() {
        currentRound += 1; 
        roundCounter.GetComponent<TextMeshPro>().text = "Round " + currentRound.ToString();

        //populate and sort the initiative queue
        List<GameObject> playerTeamSlots = playerTeam.GetComponent<Team>().teamSlots;
        initiativeQueue.AddRange(playerTeam.GetComponentsInChildren<PlacedCreature>());
        initiativeQueue.AddRange(enemyTeam.GetComponentsInChildren<PlacedCreature>());
        sortInitiativeQueue();
    }
    private void testBattle() {
        for (int i = 0; i < 6; i++) {
            placeCreature(Utils.ENEMY, i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
            placeCreature(Utils.PLAYER, i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
        }
        roundStart();
    }
}
