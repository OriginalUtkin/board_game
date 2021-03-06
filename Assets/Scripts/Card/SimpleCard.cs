﻿using System;
using UnityEngine;

public class SimpleCard : MonoBehaviour, IInteractable
{
    GameObject cardFace;
    public Guid cardGuid;
    public bool isSelected { get; set; } = false;
    public IState state { get; set; }

    public void Setup(GameObject _cardFaceTemplate, Guid _cardGuid)
    {
        cardFace = Instantiate(_cardFaceTemplate, this.transform);
        cardGuid = _cardGuid;
        this.state = new Movable(this);
    }

    private void OnMouseDown()
    {
        this.state.OnMouseDown();
    }

    private void OnMouseUp()
    {
        this.state.OnMouseUp();
    }

    private void Update()
    {
        this.state.Update();
    }

    public void ResetPosition(Vector3 startingPosition)
    {
        Debug.Log("SimpleCard.ResetPosition");
        this.transform.position = startingPosition;
    }

    public bool IsReceivable(MonoBehaviour obj)
    {
        Debug.Log("Card could not recieve any objects yet");

        return false;
    }

    public void ReceiveObject(MonoBehaviour obj)
    {
        throw new NotImplementedException();
    }
}
