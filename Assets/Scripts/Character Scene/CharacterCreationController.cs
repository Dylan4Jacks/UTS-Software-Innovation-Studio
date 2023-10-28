using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using UnityEngine.EventSystems;

//TODO
public class CharacterCreationController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_Text textError;
    public TMP_InputField inputField;
    public Button BtnSubmit;
    public GameObject LoadingScreen;
    public GameObject error;
 
    void Awake(){
    }
   

    // Start is called before the first frame update
    void Start()
    {
        BtnSubmit.onClick.AddListener(() => CreateCharacter());
        EventSystem.current.SetSelectedGameObject(null); // Deselect any previously selected object
        EventSystem.current.SetSelectedGameObject(inputField.gameObject); // Set the new selected object

        // For TMP_InputField, you might not need the OnPointerClick line.
        // But if you do, you'll need to handle it differently, perhaps by manually activating the input field.
        inputField.ActivateInputField();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
           if(EventSystem.current.currentSelectedGameObject != inputField.gameObject)
            {
                SetInputFocus();
            }
        }
        // Check if 'Enter' key is pressed
        if (!Input.GetKeyDown(KeyCode.Return))
        {
            return;
        }
        // TODO: Implement After OpenAIController works with integration
        //LoadNextScene(); // Call function to load next scene
    }

    public void SetInputFocus()
        {
            Debug.Log($"Set Focus");
            if (inputField)
            {
                Debug.Log("Set to InputField");
                EventSystem.current.SetSelectedGameObject(null); // Deselect any previously selected object
                EventSystem.current.SetSelectedGameObject(inputField.gameObject); // Set the new selected object

                inputField.ActivateInputField();

                // Position the caret at the end of the text
                inputField.caretPosition = inputField.text.Length;
            }
        }

    void CreateCharacter()
    {
        if (SingleCharacter.Instance == null) {
            SingleCharacter.Instance = new SingleCharacter();
        }

        int charLimit = 30;
        if (inputField.text.Length < charLimit) {
            Debug.Log($"Character Length Too Small. Must be Greater then {charLimit}");
            ToggleErrors($"Character Length Too Small. Must be Greater then {charLimit}");
            return;
        }

        // Start Loading animation
        //LoadingScreen.SetActive( true );
        LoadingScene(true);

        // Enemy Faction prompt addition:
        string enemyPromptPrefix = "For this prompt, generate exactly 6 cards instead. Do the opposite of the end prompt, You are to create the rival/enemy of the following prompt, so they must be opposite: ";
        
        
        StartCoroutine(MainCoroutine(enemyPromptPrefix, inputField.text));
    }

    IEnumerator MainCoroutine(string enemyPrefix, string prompt)
    {
        ModularOpenAIController modularOpenAIController = new ModularOpenAIController(this);

         List<BaseCard> PlayerCards = new List<BaseCard>();
        Coroutine playerCoroutine = StartCoroutine(modularOpenAIController.submitCharacterPrompt(prompt, PlayerCards));

        List<BaseCard> EnemyCards = new List<BaseCard>();
        Coroutine enemyCoroutine = StartCoroutine(modularOpenAIController.submitCharacterPrompt(enemyPrefix + prompt, EnemyCards));

        // Wait for both coroutines to complete
        yield return StartCoroutine(WaitForAll(playerCoroutine, enemyCoroutine));

        AfterBothFunctions(PlayerCards, EnemyCards);

    }

    IEnumerator WaitForAll(params Coroutine[] coroutines)
    {
        foreach (var coroutine in coroutines)
        {
            yield return coroutine;
        }
    }

    IEnumerator MyFunction(string parameter, List<string> resultList)
    {
        Debug.Log("Running function with parameter: " + parameter);

        // First API request
        yield return StartCoroutine(APIRequest1(parameter, resultList));

        // Using the result from the first API request for the second one
        yield return StartCoroutine(APIRequest2(resultList[0], resultList));
    }

    IEnumerator APIRequest1(string input, List<string> outputList)
    {
        // Simulate an API request with a delay
        yield return new WaitForSeconds(1);

        // For demonstration purposes, add result to the list
        outputList.Add(input + "_Output1");
    }

    IEnumerator APIRequest2(string input, List<string> outputList)
    {
        // Simulate an API request with a delay
        yield return new WaitForSeconds(1);

        // For demonstration purposes, add result to the list
        outputList.Add(input + "_Output2");
    }

    void AfterBothFunctions(List<BaseCard> PlayerCards, List<BaseCard> EnemyCards)
    {
        // Your code that should run after both function calls
        Debug.Log("Both functions completed!");
        
        if(PlayerCards.Count < 6 || EnemyCards.Count < 6){
            SingleMainThreadDispatcher.Instance.Enqueue(() => {
                Debug.Log($"Invalid Prompt. Please try a different Prompt");
            });
        
            SingleMainThreadDispatcher.Instance.Enqueue(() => {
                ToggleErrors($"Invalid Prompt. Please try a different Prompt");
            });
            SingleMainThreadDispatcher.Instance.Enqueue(() => {
                LoadingScene(false);
            });
            return;
        }
        
        SingleCharacter.Instance.cards.AddRange(PlayerCards);
        SingleCharacter.Instance.enemyCards.AddRange(EnemyCards);
        SingleCharacter.Instance.CharacterDescription = inputField.text;
        // Ensure Unity-specific code runs on the main thread
        LoadNextScene();
    }

    public void LoadingScene(bool active)
    {
        BtnSubmit.gameObject.SetActive(!active);
        error.SetActive(!active);
        LoadingScreen.SetActive(active);
    }

    void ToggleErrors(string Error)
    {
        textError.text = Error;
        // Set the opacity of the text to 100%
        Color32 currentColor = textError.color;
        currentColor.a = 255; // 255 is the maximum value for opacity
        textError.color = currentColor;
    }

        // Function to load the next scene
        public void LoadNextScene()
    {
        // Card Limit set to 0 for testing. Limit should be 7 at the minimum for a good game
        if(SingleCharacter.Instance.cards.Count < 0) 
        {
            Debug.Log("Card Count too low");
            return; 
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if next scene index is out of bounds, then reset to 0 (optional)
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // or load a specific scene, like a main menu
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
