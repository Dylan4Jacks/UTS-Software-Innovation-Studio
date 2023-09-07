using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
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
