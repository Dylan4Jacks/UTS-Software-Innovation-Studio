using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelButton : MonoBehaviour
{
    public bool pressed = false;
    public TextMeshPro buttonText;
    public GameObject highlightTexture;
    public SpriteRenderer buttonSprite;
    public Sprite buttonDefaultSprite;
    public Sprite buttonPressedSprite;
    public BattleController battleController;

    // Start is called before the first frame update
    void Start()
    {
        this.battleController = BattleController.instance;
        Debug.Log(buttonText.gameObject.transform.position.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown() {
        if (!pressed) {
            BattleController.instance.startBattle();
            pressed = true;
            setPressed();
        }
    }

    public void OnMouseEnter() {
        if (!pressed) {
            buttonText.color = Color.white;
            highlightTexture.SetActive(true);
        }
    }

    public void OnMouseExit() {
        if (!pressed) {
            buttonText.color = Color.black;
            highlightTexture.SetActive(false);
        }
    }

    void setPressed() {
        pressed = true;
        highlightTexture.SetActive(false);
        buttonSprite.sprite = buttonPressedSprite;
        buttonText.gameObject.transform.position += Vector3.down * 10;
        buttonText.color = new Color(56f,56f,56f,255f);
    }

    void setPressable() {
        pressed = false;
        buttonSprite.sprite = buttonDefaultSprite;
        buttonText.gameObject.transform.position += Vector3.up * 10;
        buttonText.color = Color.black;
    }

    public void setInitial() {
        setPressable();
        buttonText.text = "START BATTLE";
    }

    public void setGameOver() {
        buttonText.text = "GAME OVER";
    }

    public void setWaitForRound() {
        buttonText.text = "BATTLE IN PROGRESS";
    }

    public void setNextRound() {
        setPressable();
        buttonText.text = "NEXT ROUND";
    }
}
