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
    public static int ENEMY_CONTAINER_LEFT = 0;
    public static int ENEMY_CONTAINER_MID = 1;
    public static int ENEMY_CONTAINER_RIGHT = 2;
    public static int PLAYER_CONTAINER_LEFT = 3;
    public static int PLAYER_CONTAINER_MID = 4;
    public static int PLAYER_CONTAINER_RIGHT = 5;


    public static int calculateRelevantContainer(int alignment, int position) {
        if (position == 0 || position == 3) {
            if (alignment == ENEMY) {return ENEMY_CONTAINER_LEFT;}
            else if (alignment == PLAYER) {return PLAYER_CONTAINER_LEFT;}
        }
        if (position == 1 || position == 4) {
            if (alignment == ENEMY) {return ENEMY_CONTAINER_MID;}
            else if (alignment == PLAYER) {return PLAYER_CONTAINER_MID;}
        }
        if (position == 2 || position == 5) {
            if (alignment == ENEMY) {return ENEMY_CONTAINER_RIGHT;}
            else if (alignment == PLAYER) {return PLAYER_CONTAINER_RIGHT;}
        }
        Debug.Log("WARNING: invalid alignment or position");
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
}
