using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public BattleController battleController;
    public TextMeshPro textMesh;

    public void setText(string text) {
        textMesh.text = text;
    }
}
