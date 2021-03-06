﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerBattlefield : MonoBehaviour, IInteractable
{
    public GameObject[] playerSlots;
    private const int Capacity = 5;
    private const int BattlefieldCenter = PlayerBattlefield.Capacity / 2;
    private int AvailableSpots { get; set; } = PlayerBattlefield.Capacity;
    private bool IsEmpty => (this.AvailableSpots == PlayerBattlefield.Capacity);

    public bool IsReceivable(MonoBehaviour obj)
    {
        return this.AvailableSpots > 0;
    }
    public void ReceiveObject(MonoBehaviour obj)
    {
        Debug.Log("Object is received");
        GameObject spot;

        if (this.IsEmpty)
            spot = this.playerSlots[PlayerBattlefield.BattlefieldCenter];
        else
            spot = this.GetClosestSpot();

        obj.transform.parent = spot.transform;
        obj.transform.position = spot.transform.position;

        this.AvailableSpots -= 1;
    }

    private GameObject GetClosestSpot()
    {
        Debug.Log("Calculating spot coordinates");

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Dictionary<int, float> spotsDistances = this.GetAvailableSpots(cursorPosition);

        var closestIndex = spotsDistances.OrderBy(elem => elem.Value).First();

        Debug.Log("Closest slot is " + closestIndex.Key);

        return this.playerSlots[closestIndex.Key];
    }

    private Dictionary<int, float> GetAvailableSpots(Vector3 cursorPosition)
    {
        Dictionary<int, float> availableSlots = new Dictionary<int, float>();

        for (int element_index = 0; element_index < this.playerSlots.Length; element_index++)
        {
            GameObject currentSlot = this.playerSlots[element_index];

            if (currentSlot.GetComponentInChildren(typeof(SimpleCard)) == null)
                availableSlots[element_index] = Vector3.Distance(currentSlot.transform.position, cursorPosition);
        }

        return availableSlots;
    }
}
