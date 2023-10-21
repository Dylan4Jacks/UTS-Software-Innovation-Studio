using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using System;

//TODO
public class CharacterCreationController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_Text inputField;
    public Button BtnSubmit;

    ModularOpenAIController modularOpenAIController;

    // Start is called before the first frame update
    void Start()
    {
        modularOpenAIController = gameObject.AddComponent<ModularOpenAIController>();
        BtnSubmit.onClick.AddListener(() => CreateCharacter());
    }

    void Update()
    {
        // Check if 'Enter' key is pressed
        if (!Input.GetKeyDown(KeyCode.Return))
        {
            return;
        }
        // TODO: Implement After OpenAIController works with integration
        //LoadNextScene(); // Call function to load next scene
    }

    void CreateCharacter()
    {
        if (SingleCharacter.Instance == null) {
            SingleCharacter.Instance = new SingleCharacter();
        }

        if (SingleCharacter.Instance.CharacterDescription == inputField.text) {
            LoadNextScene();
        }

        int charLimit = 30;
        if(inputField.text.Length < charLimit) 
        {
            Debug.Log($"Character Length Too Small. Must be Greater then {charLimit}");
            return;
        }

        // Enemy Faction prompt addition:
        string enemyPromptPrefix = "For this prompt, generate exactly 6 cards instead. Do the opposite of the end prompt, You are to create the rival/enemy of the following prompt, so they must be opposite: ";

        //List<card>

        List<BaseCard>cards = modularOpenAIController.submitCharacterPrompt(inputField.text);
        List<BaseCard>enemyCards = modularOpenAIController.submitCharacterPrompt(enemyPromptPrefix + inputField.text);
        if(!cards.Any() || !enemyCards.Any()){
            Debug.Log($"Invalid Prompt. Please try a different Prompt");
            return;
        }
        SingleCharacter.Instance.cards.AddRange(cards);
        SingleCharacter.Instance.enemyCards.AddRange(enemyCards);
        SingleCharacter.Instance.CharacterDescription = inputField.text;
        LoadNextScene();
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
