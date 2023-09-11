using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
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
        GameObject relevantContainer = creatureContainers[Utils.calculateLane(position)];
        Transform containerTransform = relevantContainer.GetComponent<Transform>();

        GameObject placedCreature = Instantiate(BattleController.instance.creaturePrefab, containerTransform.position, Quaternion.identity);
        placedCreatures[position] = placedCreature.GetComponent<PlacedCreature>();

        Transform placedCreatureTransform =  placedCreature.GetComponent<Transform>();
        placedCreatureTransform.position += Vector3.back * 7; 
        placedCreatureTransform.position += Vector3.left * 3;
        placedCreatureTransform.position += ((position > 2)? Vector3.down : Vector3.up) * 48;

        placedCreatures[position].setupCard(baseCard, alignment, position);
        // If there's already a creature here
        if (teamSlots[position].transform.childCount > 0) {
            replaceCreature(teamSlots[position], placedCreature); 
        } else {
            placedCreature.transform.parent = teamSlots[position].transform;
        }
    }

    private void replaceCreature(GameObject teamSlot, GameObject newCreature) {
        foreach (Transform child in teamSlot.transform) {
            Destroy(child.gameObject);
        }
        newCreature.transform.parent = teamSlot.transform;
    }

    public List<PlacedCreature> getLaneCreatures(int lane) {
        List<PlacedCreature> laneCreatures = new List<PlacedCreature>();
        if (lane == Utils.LANE_LEFT && this.alignment == Utils.PLAYER) {
            laneCreatures.Add(placedCreatures[Utils.FRONT_LEFT]);
            laneCreatures.Add(placedCreatures[Utils.BACK_LEFT]); 
        } else if (lane == Utils.LANE_LEFT && this.alignment == Utils.ENEMY) {
            laneCreatures.Add(placedCreatures[Utils.BACK_LEFT]);
            laneCreatures.Add(placedCreatures[Utils.FRONT_LEFT]);
        }
        if (lane == Utils.LANE_MID && this.alignment == Utils.PLAYER) {
            laneCreatures.Add(placedCreatures[Utils.FRONT_MID]);
            laneCreatures.Add(placedCreatures[Utils.BACK_MID]); 
        } else if (lane == Utils.LANE_MID && this.alignment == Utils.ENEMY) {
            laneCreatures.Add(placedCreatures[Utils.BACK_MID]);
            laneCreatures.Add(placedCreatures[Utils.FRONT_MID]);
        }
        if (lane == Utils.LANE_RIGHT && this.alignment == Utils.PLAYER) {
            laneCreatures.Add(placedCreatures[Utils.FRONT_RIGHT]);
            laneCreatures.Add(placedCreatures[Utils.BACK_RIGHT]); 
        } else if (lane == Utils.LANE_RIGHT && this.alignment == Utils.ENEMY) {
            laneCreatures.Add(placedCreatures[Utils.BACK_RIGHT]);
            laneCreatures.Add(placedCreatures[Utils.FRONT_RIGHT]);
        }
        return laneCreatures;
    }

    public void setVictoriousLane(int lane) {
        List<PlacedCreature> laneCreatures = getLaneCreatures(lane);
        foreach (PlacedCreature laneCreature in laneCreatures) {
            laneCreature.isVictorious = true;
        }
        BattleController.instance.laneVictors[lane] = this.alignment;
    }

    public Team getAdversary() {
        if (this.alignment == Utils.PLAYER) {
            return BattleController.instance.teams[Utils.ENEMY];
        } else {
            return BattleController.instance.teams[Utils.PLAYER];
        }
    }
    public bool isLaneDefeated(int lane) {
        List<PlacedCreature> laneCreatures = getLaneCreatures(lane);
        bool laneDefeated = true;
        foreach (PlacedCreature creature in laneCreatures) {
            if (!creature.isSlain) {laneDefeated = false;}
        } 
        return laneDefeated;
    }
}

