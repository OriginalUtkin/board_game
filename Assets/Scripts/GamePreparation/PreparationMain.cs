using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;
public class PreparationMain : MonoBehaviour
{

    public CardCollection cardCollection;
    public PlayerSelection playerSelection;

    public Dictionary<Guid, GameObject> cardsInScene = new Dictionary<Guid, GameObject>();

    private PreparationLogic logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = new PreparationLogic();
        syncCards();
    }

    void syncCards()
    {
        cardCollection.SyncWithCardList(logic.collection, cardsInScene);
        playerSelection.SyncWithCardList(logic.playerSelection, cardsInScene);
    }

    public void moveCardToPlayerSelection(SimpleCard card)
    {
        logic.moveCardToPlayerSelection(card.cardGuid);
        card.transform.parent = playerSelection.transform;
        syncCards();
    }

    public void moveCardToCollection(SimpleCard card)
    {
        logic.moveCardToCollection(card.cardGuid);
        card.transform.parent = cardCollection.transform;
        syncCards();
    }

}
