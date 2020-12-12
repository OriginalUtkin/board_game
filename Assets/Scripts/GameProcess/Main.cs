using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playerBattlefield;

    private SimpleCard selectedCard;

    private PreparationLogicHolder preparationLogicHolder;
    // used for debugging
    public PreparationLogicHolder preparationLogicHolderPrefab;

    private List<Card> cards = new List<Card>();
    private Dictionary<Guid, GameObject> cardsInScene = new Dictionary<Guid, GameObject>();

    private Vector3 HandPosition
    {
        get { return this.playerHand.transform.position; }
    }

    private PreparationLogicHolder LoadPlayerSelection()
    {
        Debug.Log("load saved PreparationLogicHolder");

        PreparationLogicHolder holder = Instantiate(preparationLogicHolderPrefab, null);
        holder.name = "PreparationLogicHolder";

        PreparationLogic logic = PreparationLogic.LoadFromFile(System.IO.Path.Combine(gameObject.scene.path, "../playerSelection.xml"));
        holder.player1Preparation = logic;
        return holder;
    }

    PreparationLogicHolder GetHolder()
    {
        GameObject obj = GameObject.Find("/PreparationLogicHolder");
        PreparationLogicHolder holder;
        if (obj == null)
        {  // used for debugging
            holder = this.LoadPlayerSelection();
            obj = holder.gameObject;
        }
        else
        {
            holder = obj.GetComponent<PreparationLogicHolder>();
        }
        cards = holder.player1Preparation.playerSelection;
        return holder;
    }


    void Start()
    {
        this.preparationLogicHolder = this.GetHolder();
        this.initialisePlayerHand();
        this.initialisePlayerBattlefield();
    }

    private void initialisePlayerHand()
    {
        /* Calculate player hand start position and fill it with cards. */
        HandDrower handDrower = this.playerHand.GetComponent<HandDrower>();

        Vector3 startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.HandPosition);
        handDrower.fillStartHand(cardParent: this.playerHand, cardPosition: startCardPosition,
            cards: cards, cardsInScene: cardsInScene, clickAction: this.SelectCard);
    }

    private void initialisePlayerBattlefield()
    {
        /* Setup card movement action for the battlefield. */
        PlayerBattlefield playArea = this.playerBattlefield.GetComponent<PlayerBattlefield>();
        playArea.ClickAction = this.clickBattlefield;
    }

    public void SelectCard(SimpleCard card)
    {
        Debug.Log("Select card: " + card);
        this.selectedCard = card;
    }

    public void clickBattlefield(PlayerBattlefield battlefield)
    {
        if (this.selectedCard != null)
        {
            this.selectedCard.transform.SetParent(battlefield.playerSlots[battlefield.NextCardPosition].transform);
            this.selectedCard.transform.position = battlefield.playerSlots[battlefield.NextCardPosition].transform.position;

            battlefield.NextCardPosition += 1;
        }
    }
}
