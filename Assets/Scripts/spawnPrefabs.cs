using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPrefabs : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject hand;

    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject playerHand = Instantiate(hand, new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < 10; i++)
        {
            generateCard(playerHand);
        }
    }

    private void generateCard(GameObject hand)
    {
        GameObject createdCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        createdCard.GetComponent<CardStats>().speed = Random.Range(1, 101);
        createdCard.GetComponent<CardStats>().strength = Random.Range(1, 101);
        createdCard.GetComponent<CardStats>().health = Random.Range(1, 101);
        createdCard.transform.parent = hand.transform;
    }
}
