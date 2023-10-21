using UnityEngine;
using TMPro;
using System.Collections;

public class QuestionController : MonoBehaviour
{

    private TMP_Text textComponent;
    private float duration = 2.5f; // 2 seconds

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        // Set the text to 0% opacity at the start
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0f);
        // Start the delayed fade-in coroutine
        StartCoroutine(FadeInAfterDelay());
    }
    private IEnumerator FadeInAfterDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(0.67f);

        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;

            // Calculate alpha value
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);

            yield return null;
        }
    }
}