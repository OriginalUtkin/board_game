﻿using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playerBattlefield;

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
    }

    private void initialisePlayerHand()
    {
        /* Calculate player hand start position and fill it with cards. */
        PlayerHand hand = this.playerHand.GetComponent<PlayerHand>();

        Vector3 startCardPosition = PlayerHand.calculateStartHandPosition(handPositionCoordinate: this.HandPosition);
        hand.fillStartHand(cardParent: this.playerHand, cardPosition: startCardPosition, cards: cards, cardsInScene: cardsInScene);
    }
}
