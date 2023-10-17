using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TextResponseController : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Text tmpText;
    private TMP_InputField inputField;

    private void Awake()
    {
        Debug.Log("Awake called in TextController.");
        // Get the TMP_Text component attached to this GameObject
        tmpText = GetComponent<TMP_Text>();
        inputField = GetComponent<TMP_InputField>();

        if (inputField) {
            // Access the text component of the TMP_InputField
            tmpText = inputField.textComponent;

            if (tmpText) {
                tmpText.text = "Test";
            }
            else {
                Debug.LogError("TMP_Text component not found!");
            }
        }
        else {
            Debug.LogError("TMP_InputField component not found!");
        }
    } 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
