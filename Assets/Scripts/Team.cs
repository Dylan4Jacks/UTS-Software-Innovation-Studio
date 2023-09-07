using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Team : MonoBehaviour
{
    [SerializeField] public List<GameObject> teamSlots;
    
    void Awake() 
    {
        teamSlots = Utils.getChildren(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void assignTeamSlot(int position, GameObject card) {
        if (position <= 5 && position >= 0) {
            teamSlots[position] = card;
        }
    }
}
