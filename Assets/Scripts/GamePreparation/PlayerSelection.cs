using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;
public class PlayerSelection : MonoBehaviour
{
    public PreparationMain preparationMain;
    public CardCreator cardCreator;
    public float yOffset = 1.72f;


    public void SyncWithCardList(List<Card> cards, Dictionary<Guid, GameObject> cardsInScene)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cardsInScene.ContainsKey(cards[i].guid))
                cardsInScene[cards[i].guid] = cardCreator.createCard(cards[i], transform, preparationMain.moveCardToCollection);
            GameObject cardObj = cardsInScene[cards[i].guid];

            cardObj.GetComponent<SimpleCard>().UpdateClickAction(preparationMain.moveCardToCollection);
            cardObj.transform.parent = transform;
            cardObj.transform.localPosition = yOffset * i * Vector3.down;
        }
    }

}
