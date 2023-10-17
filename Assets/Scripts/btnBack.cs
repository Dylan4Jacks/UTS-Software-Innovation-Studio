using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnBack : MonoBehaviour
{
    public bool pressed = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [Range(0, 1)] public float fadeAmountHover = 0.6f; // Define how much you want to fade. 1 means no fade, 0 means fully transparent.
    [Range(0, 1)] public float fadeAmountPress = 0.3f; // Define how much you want to fade. 1 means no fade, 0 means fully transparent.
    private bool isMouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmountPress);
    }
    public void OnMouseUp()
    {
        if (isMouseOver) {
            LoadLastScene();
        }
    }

    public void LoadLastScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int SceneIndex = currentSceneIndex - 1;
        // Check if next scene index is out of bounds, then reset to 0 (optional)
        if (SceneIndex >= SceneManager.sceneCountInBuildSettings) {
            SceneIndex = 0; // or load a specific scene, like a main menu
        }

        SceneManager.LoadScene(SceneIndex);
    }


    public void OnMouseEnter()
    {
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmountHover);
        isMouseOver = true;
    }

    public void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
        isMouseOver = false;
    }
}
