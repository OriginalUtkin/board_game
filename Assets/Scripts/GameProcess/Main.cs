using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{    public GameObject playerHand;

    public GameObject[] playerSlots;

    private SimpleCard selectedCard;

    public SimpleCard SelectedCard{
        get { return this.selectedCard; }
    }


    private Vector3 HandPosition
    {
        get { return this.playerHand.transform.position; }
    }

    void Start()
    {
        this.initPlayerHand();
        this.initPlayerSlots();
    }

    private void initPlayerHand(){
        /* Calculate player hand start position and fill it with cards. */
        HandDrower handDrower = this.playerHand.GetComponent<HandDrower>();

        Vector3 startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.HandPosition);
        handDrower.fillStartHand(cardParent: this.playerHand, cardPosition: startCardPosition, _mainScript: this);
    }

    private void initPlayerSlots(){
        /* Setup Action field for each player slot on game board. */
    }

    public void SelectCard(SimpleCard card)
    {
        Debug.Log("playing card");
        Debug.Log(card);

        this.selectedCard = card;
        // card.transform.SetParent(playSlot.transform);
        // card.transform.position = Vector3.zero;
    }

    // public void PlaySelectedCard(SLOT_OBJECT_NOT_DEFINED cardSlot){

    // }
}
