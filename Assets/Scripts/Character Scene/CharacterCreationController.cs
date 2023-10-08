using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterController : MonoBehaviour
{

    public TextMeshProUGUI answerText; // Drag and drop your TextMeshPro object here in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Count visible characters excluding whitespace
        int visibleCharCount = answerText.text.Count(c => !char.IsWhiteSpace(c));
        
        // Check if 'Enter' key is pressed and visibleCharCount is more than 10
        if (!(Input.GetKeyDown(KeyCode.Return) && visibleCharCount > 10))
        {
            return;
        }
        
        LoadNextScene(); // Call function to load next scene
    }

    // Function to load the next scene
    public void LoadNextScene()
    {
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
