using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPrefabs : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject hand;
    public SingleCharacter singleCharacter;

    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject playerHand = Instantiate(hand, new Vector3(0, 0, 0), Quaternion.identity);
        if(singleCharacter.cards.Count > 7) 
        {
            foreach (BaseCard card in singleCharacter.cards)
            {
                generateCardFromStatsObject(playerHand, card);
            }
        }
        else {
            for(int i = 0; i < 10; i++) 
            {
                generateCard(playerHand);
            }
        }
    }
    private void generateCardFromStatsObject(GameObject hand, BaseCard card)
    {
        GameObject createdCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        createdCard.GetComponent<Card>().speed = card.speed;
        createdCard.GetComponent<Card>().strength = card.strength;
        createdCard.GetComponent<Card>().health = card.health;
        createdCard.transform.parent = hand.transform;

    }
    private void generateCard(GameObject hand)
    {
        GameObject createdCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        createdCard.GetComponent<Card>().speed = Random.Range(1, 101);
        createdCard.GetComponent<Card>().strength = Random.Range(1, 101);
        createdCard.GetComponent<Card>().health = Random.Range(1, 101);
        createdCard.transform.parent = hand.transform;
    }
}
