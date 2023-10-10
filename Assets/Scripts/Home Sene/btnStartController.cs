using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class btnStartController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white; // The color of text normally
    public Color hoverColor = Color.red;    // The color of text on hover
    public TMP_Text txtStartText;
    [SerializeField] private float flashSpeed = 1f;
    [SerializeField] private float minAlpha = 0f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] [Range(0, 2)] private float VisiblePerc = 1; // New variable

    //Flashing Text Private values
    private float timer;

    private void Awake()
    {
    float fadeInPerc = 0.5f - VisiblePerc * 0.25f;
    timer = fadeInPerc / flashSpeed; // Initialize timer to the point where the text is fully visible  
    }

    private void Start()
    {
        txtStartText.color = normalColor; // Set initial color
    }
    private void Update()
    {
        timer += Time.deltaTime * flashSpeed;
        float lerpTime = Mathf.PingPong(timer, 1);
        float fadeInPerc = 0.5f - VisiblePerc * 0.25f;
        float fadeOutPerc = 0.5f + VisiblePerc * 0.25f;

        float modifiedLerpTime;
        if (lerpTime < fadeInPerc)
        {
            modifiedLerpTime = lerpTime / fadeInPerc;
        }
        else if (lerpTime < fadeOutPerc)
        {
            modifiedLerpTime = 1;
        }
        else
        {
            modifiedLerpTime = 1 - (lerpTime - fadeOutPerc) / (1 - fadeOutPerc);
        }

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, modifiedLerpTime);
        txtStartText.color = new Color(txtStartText.color.r, txtStartText.color.g, txtStartText.color.b, alpha);
    }


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

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtStartText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtStartText.color = normalColor;
    }
}
