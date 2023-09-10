using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /*************************************************************
    * GLOBAL CONSTANTS 
    * for easy referencing by name instead of putting in a number
    **************************************************************/
    // for referencing alignment
    public static int ENEMY = 0;
    public static int PLAYER = 1;

    // for referencing position relative to alignment
    public static int FRONT_LEFT = 0;
    public static int FRONT_MID = 1;
    public static int FRONT_RIGHT = 2;
    public static int BACK_LEFT = 3;
    public static int BACK_MID = 4;
    public static int BACK_RIGHT = 5;

    // for referencing the relevant container element 
    public static int CONTAINER_LEFT = 0;
    public static int CONTAINER_MID = 1;
    public static int CONTAINER_RIGHT = 2;


    public static int calculateRelevantContainer(int position) {
        return position == FRONT_LEFT || position == BACK_LEFT? CONTAINER_LEFT : 
               position == FRONT_MID || position == BACK_MID? CONTAINER_MID : CONTAINER_RIGHT;
    }
    public static int calculateRowPartner(int position) {
        if (position == FRONT_LEFT) { return BACK_LEFT; }
        if (position == BACK_LEFT) { return FRONT_LEFT; }
        if (position == FRONT_MID) { return BACK_MID; }
        if (position == BACK_MID) { return FRONT_MID; }
        if (position == FRONT_MID) { return BACK_MID; }
        if (position == BACK_MID) { return FRONT_MID; }
        return 0;
    }

    //This gets all the children, including children of children. This populates the list depth first. 
    public static List<GameObject> getAllChildren(GameObject obj) {
    		List<GameObject> children = new List<GameObject>();
    
    		foreach(Transform child in obj.transform) {
    			children.Add(child.gameObject);
    			children.AddRange(getAllChildren(child.gameObject));
    		}
    		return children;
    	} 

    public static List<GameObject> getChildren(GameObject obj) {
        List<GameObject> children = new List<GameObject>(); 
        foreach(Transform child in obj.transform) {
            children.Add(child.gameObject);
        }
        return children;
    }

    //TODO: write unit test for this one
    public static int ComparePlacedCreaturesBySpeed(PlacedCreature a, PlacedCreature b) {
        return a == null? (b == null? 0 : -1): 
            (b == null? 1 : a.currentSpeed > b.currentSpeed? 1: 
                (a.currentSpeed < b.currentSpeed? -1 : 0)); 
    }
}
