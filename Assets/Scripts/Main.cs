using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playSlot;
    public GameObject playerHand;

    private Vector3 HandPosition{
        get { return this.playerHand.transform.position; }
    }

    void Start()
    {
        HandDrower handDrower = this.playerHand.GetComponent<HandDrower>();

        Vector3 startCardPosition = HandDrower.calculateStartHandPosition(handPositionCoordinate: this.HandPosition);
        handDrower.fillStartHand(cardParent: this.playerHand, cardPosition: startCardPosition, _mainScript: this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCard(SimpleCard card) {
        Debug.Log("playing card");
        Debug.Log(card);
        card.transform.SetParent(playSlot.transform);
        card.transform.position = Vector3.zero;
    }
}
