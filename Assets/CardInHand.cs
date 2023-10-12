using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInHand : MonoBehaviour
{
    public BaseCard baseCard; 
    public TextMeshPro cardNameText;
    public TextMeshPro cardAttackValueText;
    public TextMeshPro cardHealthValueText;

    public Vector3 originalPos;
    public Quaternion originalRotate;

    public PlayerHand hand;
    private Boolean isSelected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialise(BaseCard card, PlayerHand playerHand) {
        this.baseCard = card;
        cardNameText.text = card.cardName;
        cardAttackValueText.text = card.strength.ToString();
        cardHealthValueText.text = card.health.ToString();
        gameObject.name = baseCard.cardName;

        this.originalPos = gameObject.transform.position;
        this.originalRotate = gameObject.transform.rotation;
        this.hand = playerHand;
    }
    
    void OnMouseEnter() {
        gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }

    void OnMouseExit() {
        gameObject.transform.rotation = originalRotate;
    }

    public void OnMouseDown() {
        if (!isSelected) {
            if (hand.selectedCard != null) { hand.selectedCard.unSelect(); }
            hand.setSelectedCard(this);
            setSelected();
        } else {
            hand.selectedCard = null;
            unSelect();
        }
    }

    void setSelected() {
        this.isSelected = true;
        gameObject.transform.position += Vector3.up * 50;
    }

    public void unSelect() {
        this.isSelected = false;
        gameObject.transform.position += Vector3.down * 50;
    }
}
