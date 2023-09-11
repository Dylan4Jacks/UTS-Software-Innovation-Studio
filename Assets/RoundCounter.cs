using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public BattleController battleController;
    public TextMeshPro textMesh;
    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Round " + battleController.currentRound.ToString();
    }
}
