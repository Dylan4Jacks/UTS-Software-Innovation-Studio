using UnityEngine;
using TMPro;
using System.Collections;

public class QuestionController : MonoBehaviour
{

    private TMP_Text textComponent;
    private float duration = 2f; // 2 seconds
    private float elapsedTime = 0f;
    private bool isFadingIn = true;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        // Set the text to 0% opacity at the start
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0f);
    }
    private void Update()
    {
        if (isFadingIn) {
            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate alpha value
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);

            // Check if fading is completed
            if (alpha >= 1f) {
                isFadingIn = false;
            }
        }
    }
}