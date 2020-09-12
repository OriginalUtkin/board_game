using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;
public class CardCollection : MonoBehaviour
{
    public CardCreator cardCreator;

    public int rowSize = 4;
    public float xOffset = -1.72f;
    public float yOffset = 1.72f;

    public void SyncWithCardList(List<Card> cards)
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
            GameObject cardObj = cardCreator.createCard(cards[i], transform);
            cardObj.transform.position += xOffset * currentRowSize * Vector3.right;
            cardObj.transform.position += yOffset * rowNumber * Vector3.down;
            currentRowSize++;

        }
    }
}
