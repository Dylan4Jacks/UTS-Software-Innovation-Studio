using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FocusInputField : MonoBehaviour
{
    public TMP_InputField inputField;

    public void SetFocus()
    {
        if (inputField)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }
    }
}
