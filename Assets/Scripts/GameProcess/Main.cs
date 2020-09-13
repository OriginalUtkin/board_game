using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Main : MonoBehaviour
{    public GameObject playerHand;
     public GameObject playerBattlefield;

    private SimpleCard selectedCard;

      private Vector3 HandPosition
    {
        get { return this.playerHand.transform.position; }
    }

    void Start()
    {
        this.initialisePlayerHand();
        this.initialisePlayerBattlefield();
    }

    private void initialisePlayerHand(){
        /* Calculate player hand start position and fill it with cards. */
        HandDrower handDrower = this.playerHand.GetComponent<HandDrower>();

        Vector3 startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.HandPosition);
        handDrower.fillStartHand(cardParent: this.playerHand, cardPosition: startCardPosition, _mainScript: this);
    }

    private void initialisePlayerBattlefield(){
        /* Setup card movement action for the battlefield. */
        PlayerBattlefield playArea = this.playerBattlefield.GetComponent<PlayerBattlefield>();
        playArea.ClickAction = this.clickBattlefield;
    }

    public void SelectCard(SimpleCard card)
    {
        Debug.Log("Select card: " + card);
        this.selectedCard = card;
    }

    public void clickBattlefield(PlayerBattlefield battlefield){
        if (this.selectedCard != null){
            this.selectedCard.transform.SetParent(battlefield.playerSlots[battlefield.NextCardPosition].transform);
            this.selectedCard.transform.position = battlefield.playerSlots[battlefield.NextCardPosition].transform.position;      
            
            battlefield.NextCardPosition += 1;
        } 
    }
}
