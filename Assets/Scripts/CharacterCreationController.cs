using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterCreationController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button submitCharacterButton;
    public 

    // Start is called before the first frame update
    void Start()
    {
        submitCharacterButton.onClick.AddListener(() => LoadNextQuestion());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            LoadNextQuestion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene("Scenes/Character Creation Scene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene("Scenes/Battle Scene");
        }
    }

    private void LoadNextQuestion()
    {
        //Save Current Response
        //Load Next Question
        textField.text = "NEXT QUESTION";
    }

    
    
}
