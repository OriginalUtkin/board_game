using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;

public class Main : MonoBehaviour
{
    public HandDrower player1Hand, player2Hand;
    public PlayerBattlefield playerBattlefield;

    private SimpleCard selectedCard;

    private PreparationLogicHolder preparationLogicHolder;
    // used for debugging
    public PreparationLogicHolder preparationLogicHolderPrefab;

    private List<Card> player1Cards = new List<Card>();
    private List<Card> player2Cards = new List<Card>();
    private Dictionary<Guid, GameObject> cardsInScene = new Dictionary<Guid, GameObject>();

    private Vector3 Player1HandPosition
    {
        get { return this.player1Hand.transform.position; }
    }

    private Vector3 Player2HandPosition
    {
        get { return this.player2Hand.transform.position; }
    }

    private PreparationLogicHolder LoadPlayerSelections()
    {
        PreparationLogicHolder holder;
        Debug.Log("loading saved PreparationLogicHolder");

        holder = Instantiate(preparationLogicHolderPrefab, null);
        holder.name = "PreparationLogicHolder";

        holder.player1Preparation = PreparationLogic.LoadFromFile(System.IO.Path.Combine(gameObject.scene.path, "../player1Preparation.xml"));
        holder.player2Preparation = PreparationLogic.LoadFromFile(System.IO.Path.Combine(gameObject.scene.path, "../player2Preparation.xml"));
        return holder;
    }

    PreparationLogicHolder GetHolder()
    {
        GameObject obj = GameObject.Find("/PreparationLogicHolder");
        PreparationLogicHolder holder;
        if (obj == null)
        {  // used for debugging
            holder = this.LoadPlayerSelections();
            obj = holder.gameObject;
        }
        else
        {
            holder = obj.GetComponent<PreparationLogicHolder>();
        }
        player1Cards = holder.player1Preparation.playerSelection;
        player2Cards = holder.player2Preparation.playerSelection;
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
        Vector3 startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.Player1HandPosition);
        player1Hand.fillStartHand(cardParent: this.player1Hand.transform, cardPosition: startCardPosition,
            cards: player1Cards, cardsInScene: cardsInScene, clickAction: this.SelectCard);

        startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.Player2HandPosition);
        player2Hand.fillStartHand(cardParent: this.player2Hand.transform, cardPosition: startCardPosition,
            cards: player2Cards, cardsInScene: cardsInScene, clickAction: this.SelectCard);
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
