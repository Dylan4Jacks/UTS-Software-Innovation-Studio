using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public BattleController battleController;
    protected int currentPage = 1;
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

    public void updateUI() {
        List<PlacedCreature> initiativeQueue = battleController.initiativeQueue;
        int queueSize = initiativeQueue.Count;
        int pagesInQueue = queueSize%6 == 0? queueSize/6 : queueSize/6 + 1; 
        if (currentPage > pagesInQueue) {currentPage = pagesInQueue;}
        int index = (currentPage - 1)*6; 
        int endIndex = currentPage*6 <= queueSize? currentPage*6: queueSize;
        foreach (InitiativeQueueSlot slot in slots) {
            if (index >= endIndex) {slot.empty();} 
            else {slot.setCreature(initiativeQueue[index]);}
            index++;
        }
        validateNavArrows(pagesInQueue); 
    }

    private void validateNavArrows(int pagesInQueue) {
        if (currentPage == 1) {upArrow.gameObject.SetActive(false);}
        if (currentPage == pagesInQueue) {downArrow.gameObject.SetActive(false);}
        if (pagesInQueue > currentPage) {downArrow.gameObject.SetActive(true);}
        if (currentPage > 1) {upArrow.gameObject.SetActive(true);} 
    }

    public void clickUp() {
        currentPage -= 1;
        updateUI();
    }

    public void clickDown() {
        currentPage += 1;
        updateUI();
    }
}
