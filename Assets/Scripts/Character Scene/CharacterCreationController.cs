using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

//TODO
public class CharacterCreationController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_Text inputField;
    public Button BtnSubmit;

    ModularOpenAIController modularOpenAIController;

    public SingleCharacter singleCharacter;
    // Start is called before the first frame update
    void Start()
    {
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
        int charLimit = 30;
        if(inputField.text.Length < charLimit) 
        {
            Debug.Log($"Character Length Too Small. Must be Greater then {charLimit}");
            return;
        }

        //List<card>
        List<BaseCards> cards = modularOpenAIController.submitCharacterPrompt(inputField.text);
        singleCharacter.cards.AddRange(cards);
        LoadNextScene();
    }

    // Function to load the next scene
    public void LoadNextScene()
    {
        // Card Limit set to 0 for testing. Limit should be 7 at the minimum for a good game
        if(singleCharacter.cards.Count < 0) 
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
