using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;
public class CardCollection : MonoBehaviour
{
    public CardCreator cardCreator;
    public PreparationMain preparationMain;
    public int rowSize = 4;
    public float xOffset = -1.72f;
    public float yOffset = 1.72f;


    public void SyncWithCardList(List<Card> cards, Dictionary<Guid, GameObject> cardsInScene)
    {
        int currentRowSize = 0;
        int rowNumber = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            if (currentRowSize == rowSize)
            {
                rowNumber++;
                currentRowSize = 0;
            }

            if (!cardsInScene.ContainsKey(cards[i].guid))
                cardsInScene[cards[i].guid] = cardCreator.createCard(cards[i], transform, preparationMain.moveCardToPlayerSelection);
            GameObject cardObj = cardsInScene[cards[i].guid];

            cardObj.GetComponent<SimpleCard>().UpdateClickAction(preparationMain.moveCardToPlayerSelection);
            cardObj.transform.localPosition = xOffset * currentRowSize * Vector3.right;
            cardObj.transform.localPosition += yOffset * rowNumber * Vector3.down;
            currentRowSize++;
        }
    }

}
