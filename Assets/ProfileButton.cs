using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    public GameObject highlighted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter() {
        highlighted.SetActive(true);
        InfoPanelController.instance.setPromptReminderView();
    }

    public void OnMouseExit() {
        highlighted.SetActive(false);
        InfoPanelController.instance.returnToDefault("");
    }
}
