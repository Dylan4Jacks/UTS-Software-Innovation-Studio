using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Utils
{
    /*************************************************************
    * GLOBAL CONSTANTS 
    * for easy referencing by name instead of putting in a number
    **************************************************************/
    // for referencing alignment
    public static int ENEMY = 0;
    public static int PLAYER = 1;
    public static int NO_ALIGNMENT = 2;

    // for referencing position
    // WARNING: these positions do not flip when referencing the enemy.
    // eg. BACK_RIGHT will be the enemy's FRONT_RIGHT (in the context of battle)
    public static int FRONT_LEFT = 0;
    public static int FRONT_MID = 1;
    public static int FRONT_RIGHT = 2;
    public static int BACK_LEFT = 3;
    public static int BACK_MID = 4;
    public static int BACK_RIGHT = 5;

    // for referencing the relevant LANE element & lanes
    public static int LANE_LEFT = 0;
    public static int LANE_MID = 1;
    public static int LANE_RIGHT = 2;

    // for referencing the row a unit is on
    // WARNING - this is independant from the position reference & 
    // is not dependent on alignment.
    public static int FRONT_ROW = 0;
    public static int BACK_ROW = 1;


    public static int calculateLane(int position) {
        return position == FRONT_LEFT || position == BACK_LEFT? LANE_LEFT : 
               position == FRONT_MID || position == BACK_MID? LANE_MID : LANE_RIGHT;
    }
    public static int calculateRow(int position, int alignment) {
        if (alignment == PLAYER) {
            if (position <= FRONT_RIGHT) {return FRONT_ROW;}
            else {return BACK_ROW;}
        } else {
            if (position >= BACK_LEFT) {return FRONT_ROW;}
            else {return BACK_ROW;}
        }
    }
    public static int calculateLanePartner(int position) {
        if (position == FRONT_LEFT) { return BACK_LEFT; }
        else if (position == BACK_LEFT) { return FRONT_LEFT; }
        else if (position == FRONT_MID) { return BACK_MID; }
        else if (position == BACK_MID) { return FRONT_MID; }
        else if (position == FRONT_RIGHT) { return BACK_RIGHT; }
        else { return FRONT_RIGHT; }
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
        return a == null? (b == null? 0 : 1): 
            (b == null? -1 : a.currentSpeed > b.currentSpeed? -1: 
                (a.currentSpeed < b.currentSpeed? 1 : 0)); 
    }
    public static void ClearConsole() {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
        System.Type type = assembly.GetType("UnityEditor.LogEntries");
        System.Reflection.MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public static string roundTemplate() {
        return "Round " + BattleController.instance.currentRound.ToString() + ": ";
    }

    public static string alignmentString(int alignment) {
        string text = "ERROR";
        if (alignment == ENEMY) {
            text = "Enemy";
        }
        if (alignment == PLAYER) {
            text = "Player";
        }
        if (alignment == NO_ALIGNMENT) {
            text = "Neither";
        }
        return text;
    }
}
