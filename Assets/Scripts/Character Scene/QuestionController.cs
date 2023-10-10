using UnityEngine;
using TMPro;
using System.Collections;

public class QuestionController : MonoBehaviour
{
    public float delayBeforeStart = 0.1f;
    public float fadeInTime = 0.1f;
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;
        
        yield return new WaitForSeconds(delayBeforeStart);

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            float counter = 0f;
            Color32[] newVertexColors = textComponent.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;
            Color32 c = newVertexColors[charInfo.vertexIndex + 0];

            while (counter < fadeInTime)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, counter / fadeInTime);
                c.a = (byte)(alpha * 255);
                for (int j = 0; j < 4; ++j)
                    newVertexColors[charInfo.vertexIndex + j] = c;
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                yield return null;
            }
            yield return null;
        }
    }
}