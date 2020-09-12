using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using UnityEngine;
public class PlayerSelection : MonoBehaviour
{
    public CardCreator cardCreator;
    public float yOffset = 1.72f;


    public void SyncWithCardList(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = cardCreator.createCard(cards[i], transform);
            cardObj.transform.position += yOffset * i * Vector3.down;
        }
    }
}
