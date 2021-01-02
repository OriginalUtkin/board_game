using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattlefield : MonoBehaviour, IInteractable
{
    public GameObject[] playerSlots;

    public int NextCardPosition { get; set; } = 0;

    public Action<PlayerBattlefield> ClickAction { get; set; }

    private void OnMouseDown()
    {
        Debug.Log("Player battlefield is clicked");
    }

    public void ReceiveObject(MonoBehaviour obj)
    {
        Debug.Log("Object is received");
    }

}
