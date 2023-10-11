using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<GameObject> hand;
    public GameObject cardPrefab;
    public CardInHand selectedCard;
    public static PlayerHand instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }

        List<BaseCard> cards = new List<BaseCard>();
        cards.Add(new BaseCard("Woah", 2, 2, 2));
        cards.Add(new BaseCard("Yeah", 3, 3, 3));
        cards.Add(new BaseCard("Ohno", 4, 4, 4));

        spawnHand(cards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnHand(List<BaseCard> cards) {
        int cardCount = 0;
        foreach (BaseCard card in cards) {
            cardCount++;
            //instantiate a new cardinhand
            GameObject cardInHand = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Transform cardTransform = cardInHand.GetComponent<Transform>();
            cardTransform.position += Vector3.back * 20 + Vector3.back * cardCount * 7; //to stop overlapping
            cardTransform.position += Vector3.down * 345; 
            cardTransform.position += Vector3.left * 400;
            cardTransform.position += Vector3.right * 115 * (cardCount - 1); 
            cardTransform.rotation = Quaternion.AngleAxis(7 - cardCount, Vector3.forward);
            cardTransform.localScale = new Vector3(0.4f,0.4f,0.4f);
            cardInHand.GetComponent<CardInHand>().initialise(card, this);

            cardInHand.transform.parent = gameObject.transform;

            //set the cardinhand as a child object
        }
    }

    public void setSelectedCard(CardInHand card) {
        this.selectedCard = card;
    }
    
    public void placeCreature(int team, int position) {
        BattleController.instance.teams[team].placeCreature(position, this.selectedCard.baseCard);
    }
}
