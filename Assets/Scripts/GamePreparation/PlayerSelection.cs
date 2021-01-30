using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;
public class PlayerSelection : MonoBehaviour, IInteractable
{
    public float yOffset = 1.72f;


    public void SyncWithCardList(List<Card> cards, Dictionary<Guid, GameObject> cardsInScene)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = CardCreator.Instance.getCard(cardsInScene, cards[i], transform);

            cardObj.transform.parent = transform;
            cardObj.transform.localPosition = yOffset * i * Vector3.down;
        }
    }

    public void ReceiveObject(MonoBehaviour obj)
    {
        SimpleCard card = obj.GetComponent<SimpleCard>();
        if (card == null)
            return;

        Debug.Log("PlayerSelection.ReceivedCard", card);
        PreparationMain.Instance.MoveCardToPlayerSelection(card);
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
