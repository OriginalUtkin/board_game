using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;

public class HandDrower : MonoBehaviour
{
    public readonly static int initialDrawSize = 3;
    public readonly static int handLimit = 5;

    public GameObject simpleCardPrefab;
    public GameObject[] facesPrefabs;
    public CardCreator cardCreator;

    public static Vector3 calculateStartHandPosition(Vector3 handPositionCoordinate)
    {
        int left_side = HandDrower.handLimit / 2;
        return handPositionCoordinate + Vector3.left * left_side * 1.5f;
    }


    public void fillStartHand(GameObject cardParent, Vector3 cardPosition, List<Card> cards, Dictionary<Guid, GameObject> cardsInScene)
    {
        int offset = 2;

        for (int card_counter = 0; card_counter < HandDrower.initialDrawSize; card_counter++)
        {
            if (!cardsInScene.ContainsKey(cards[card_counter].guid))
                cardsInScene[cards[card_counter].guid] = cardCreator.createCard(cards[card_counter], cardParent.transform);

            GameObject newCard = cardsInScene[cards[card_counter].guid];
            newCard.transform.position = cardPosition;
            cardPosition.x += offset;
        }
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
