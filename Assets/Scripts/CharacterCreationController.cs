using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterCreationController : MonoBehaviour
{
    public TMP_Text txtQuestion;
    public TMP_InputField inAnswer;
    public Button btnContinue;
    private SingleCharacter singleCharacter = SingleCharacter.Instance;


    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(() => LoadNextQuestion());
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
        txtQuestion.text = "NEXT QUESTION";
    }

    
    
}
