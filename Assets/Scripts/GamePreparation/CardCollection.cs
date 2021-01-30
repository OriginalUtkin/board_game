using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;

public class CardCollection : MonoBehaviour, IInteractable
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

            GameObject cardObj = cardCreator.getCard(cardsInScene, cards[i], transform);

            cardObj.transform.parent = transform;
            cardObj.transform.localPosition = xOffset * currentRowSize * Vector3.right;
            cardObj.transform.localPosition += yOffset * rowNumber * Vector3.down;
            currentRowSize++;
        }
    }


    public void ReceiveObject(MonoBehaviour obj)
    {
        SimpleCard card = obj.GetComponent<SimpleCard>();
        if (card == null)
            return;

        Debug.Log("CardCollection.ReceivedCard", card);
        preparationMain.MoveCardToCollection(card);
        card.state = new Movable(card);
    }

    public bool IsReceivable(MonoBehaviour obj)
    {
        SimpleCard[] currentCards = GetComponentsInChildren<SimpleCard>();

        foreach (SimpleCard card in currentCards)
            if (card.gameObject == obj.gameObject)
                return false;

        return true;
    }


}
