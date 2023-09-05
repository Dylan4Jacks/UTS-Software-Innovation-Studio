using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonClicker : MonoBehaviour
{
    UIDocument buttonDocument;
    
    Button uiButton;

    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        if(buttonDocument == null)
        {
            Debug.LogError("No button document found");
        }

        uiButton = buttonDocument.rootVisualElement.Q("TestButton") as Button;

        if(uiButton != null)
        {
            Debug.Log("Button Found");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
