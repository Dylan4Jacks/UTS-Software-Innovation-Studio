using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleController : MonoBehaviour
{
    public List<BaseCard> playerDeck; //TODO 
    public GameObject creaturePrefab;
    public List<GameObject> creatureContainers; 
    public GameObject enemyTeam;
    public GameObject playerTeam;
    public List<Card> initiativeQueue = new List<Card>();

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
    } 

    private void testBattle() {
        // place the creatures first 
        placeCreature(Utils.ENEMY, Utils.FRONT_MID, new BaseCard("Name", 2, 2, 2));
        placeCreature(Utils.ENEMY, Utils.BACK_RIGHT, new BaseCard("Name", 5, 2, 60));
        placeCreature(Utils.PLAYER, Utils.BACK_MID, new BaseCard("Name", 3, 67, 2));
    }
}
