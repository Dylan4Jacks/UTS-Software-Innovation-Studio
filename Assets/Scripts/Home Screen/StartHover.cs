using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class StartHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white; // The color of text normally
    public Color hoverColor = Color.red;    // The color of text on hover
    public TMP_Text textMeshPro;
    private void Start()
    {
        textMeshPro.color = normalColor; // Set initial color
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.color = normalColor;
    }
}
