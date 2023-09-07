using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleController : MonoBehaviour
{
    public int currentRound = 1;
    public List<BaseCard> playerDeck; //TODO 
    public GameObject creaturePrefab;
    public List<GameObject> creatureContainers; 
    public GameObject enemyTeam;
    public GameObject playerTeam;
    public List<PlacedCreature> initiativeQueue = new List<PlacedCreature>();

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
        placedCreature.transform.parent = playerTeam.GetComponent<Team>().teamSlots[position].transform;
        initiativeQueue.Add(placedCreature.GetComponent<PlacedCreature>());
        sortInitiativeQueue();
    } 

    private void sortInitiativeQueue() {
        initiativeQueue.Sort(Utils.ComparePlacedCreaturesBySpeed);
    }

    private void testBattle() {
        for (int i = 0; i < 6; i++) {
            placeCreature(Utils.ENEMY, i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
            placeCreature(Utils.PLAYER, i, new BaseCard("Name", Random.Range(1, 101), Random.Range(1, 101), Random.Range(1, 101)));
        }
    }
}
