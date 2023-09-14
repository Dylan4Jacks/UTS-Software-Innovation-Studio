using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InitiativeQueueUI : MonoBehaviour
{
    public static InitiativeQueueUI instance;
    public List<InitiativeQueueSlot> slots = new List<InitiativeQueueSlot>();
    public List<GameObject> selectCreatureBox = new List<GameObject>(); 
    public GameObject initiativeQueueObject;
    public NavArrow upArrow; 
    public NavArrow downArrow;

    protected int startElement = 0;
    protected int queueLength;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    public void handleNewQueue(List<PlacedCreature> creaturesQueue, BattleController battleController){
        queueLength = creaturesQueue.Count;
        if (creaturesQueue.Count > 6) {
            downArrow.gameObject.SetActive(true);
        }
        for (int i = 0; i < creaturesQueue.Count; i++) {
            int creaturePosition = creaturesQueue[i].getPosition();
            int creatureAlignment = creaturesQueue[i].getAlignment();
            
            slots[i].creature = creaturesQueue[i];

            Team team = battleController.teams[creatureAlignment];
            GameObject relevantContainer = team.creatureContainers[Utils.calculateLane(creaturePosition)];
            slots[i].creatureSelectionBox = Utils.getChildren(relevantContainer)[creaturePosition > 2? 1 : 0];
        }
    } 

    private void moveDown(int numSlots) {
        //move down by number of slots
        if ((startElement + 6) >= queueLength) {
            return;
        }
        initiativeQueueObject.GetComponent<Transform>().position += Vector3.up * 68f * numSlots; 
        startElement += numSlots;
        if (startElement > 0) {
            upArrow.gameObject.SetActive(true);
        }
        if (startElement + 6 == queueLength) {
            downArrow.gameObject.SetActive(false);
        }
        if ((startElement + 6) < queueLength) {
            downArrow.gameObject.SetActive(true);
        }
    }

    private void moveUp(int numSlots) {
        //move up by number of slots
        if (startElement == 0) {
            return;
        }
        initiativeQueueObject.GetComponent<Transform>().position += Vector3.down * 68f * numSlots; 
        startElement -= numSlots;
        if (startElement == 0) {
            upArrow.gameObject.SetActive(false);
        }
        if ((startElement + 6) < queueLength) {
            downArrow.gameObject.SetActive(true);
        }
    }

    public void clickUp() {
        moveUp(1);
    }

    public void clickDown() {
        moveDown(1);
    }
}
