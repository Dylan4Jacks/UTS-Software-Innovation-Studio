using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public GameObject defaultView;
    public GameObject creatureView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCreatureView() {
        creatureView.SetActive(true);
        defaultView.SetActive(false);
    }
    
    public void setDefaultView() {
        creatureView.SetActive(false);
        defaultView.SetActive(true);
    }
}
