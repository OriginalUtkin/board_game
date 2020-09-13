using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattlefield : MonoBehaviour
{
    public GameObject[] playerSlots;

    public int NextCardPosition { get; set; } = 0;

    public Action<PlayerBattlefield> ClickAction { get; set; }

    private void OnMouseDown()
    {
        Debug.Log("Player battlefield is clicked");
        this.ClickAction(this);
    }

}
