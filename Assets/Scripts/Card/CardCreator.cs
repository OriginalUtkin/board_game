using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject[] facesPrefabs;

    Dictionary<string, GameObject> facesPrefabsDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        // TODO solve it in nicer way
        facesPrefabsDict.Add(Card.Goblin, facesPrefabs[0]);
        facesPrefabsDict.Add(Card.Gnome, facesPrefabs[1]);
        facesPrefabsDict.Add(Card.Elf, facesPrefabs[2]);
        facesPrefabsDict.Add(Card.Demon, facesPrefabs[3]);
    }

    public GameObject createCard(Card card, Transform parent)
    {
        GameObject newCard = Instantiate(cardPrefab, parent);
        newCard.GetComponent<SimpleCard>().Setup(facesPrefabsDict[card.Name], card.guid);
        return newCard;
    }

    public GameObject getCard(Dictionary<Guid, GameObject> cardsInScene, Card card, Transform parent)
    {
        if (!cardsInScene.ContainsKey(card.guid))
            cardsInScene[card.guid] = this.createCard(card, parent);

        return cardsInScene[card.guid];
    }

}
