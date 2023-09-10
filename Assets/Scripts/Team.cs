using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
public class Team : MonoBehaviour
{
    [SerializeField] public List<GameObject> teamSlots;
    public List<PlacedCreature> placedCreatures = new List<PlacedCreature>(new PlacedCreature[6]);
    public List<GameObject> creatureContainers; 
    public int alignment;
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
    public void placeCreature(int position, BaseCard baseCard) {
        GameObject relevantContainer = creatureContainers[Utils.calculateRelevantContainer(position)];
        Transform containerTransform = relevantContainer.GetComponent<Transform>();

        GameObject placedCreature = Instantiate(BattleController.instance.creaturePrefab, containerTransform.position, Quaternion.identity);
        placedCreatures[position] = placedCreature.GetComponent<PlacedCreature>();

        Transform placedCreatureTransform =  placedCreature.GetComponent<Transform>();
        placedCreatureTransform.position += Vector3.back * 7; 
        placedCreatureTransform.position += Vector3.left * 3;
        placedCreatureTransform.position += ((position > 2)? Vector3.down : Vector3.up) * 48;

        placedCreatures[position].setupCard(baseCard, alignment, position);
        placedCreature.transform.parent = teamSlots[position].transform;
    }
}

