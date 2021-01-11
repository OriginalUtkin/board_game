using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattlefield : MonoBehaviour, IInteractable
{
    public GameObject[] playerSlots;

    private const int Capacity = 5;

    private int AvailableSpots { get; set; } = PlayerBattlefield.Capacity;

    private void OnMouseDown()
    {
        Debug.Log("Player battlefield is clicked");
    }

    public void ReceiveObject(MonoBehaviour obj)
    {
        Debug.Log("Object is received");
        int cardPosition;

        if (this.AvailableSpots == PlayerBattlefield.Capacity)
            cardPosition = PlayerBattlefield.Capacity / 2;
        else
            cardPosition = this.CalculateCardPosition();

        obj.transform.position = this.playerSlots[cardPosition].transform.position;

        this.AvailableSpots -= 1;
    }

    private int CalculateCardPosition()
    {
        Debug.Log("Calculating spot coordinates");
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        double closestDistance = Vector3.Distance(this.playerSlots[0].transform.position, cursorPosition);
        int closestIndex = 0;

        for (int i = 1; i < this.playerSlots.Length; i++)
        {
            double distance = Vector3.Distance(this.playerSlots[i].transform.position, cursorPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        Debug.Log("Closest slot is " + closestIndex);

        return closestIndex;
    }

    public bool IsReceivable()
    {
        return this.AvailableSpots > 0;
    }

}
