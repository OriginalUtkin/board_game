using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCard : MonoBehaviour
{
    GameObject cardFace;
    Action<SimpleCard> clickAction;
    public Guid cardGuid;
    public bool isSelected { get; private set; } = false;

    public void Setup(GameObject _cardFaceTemplate, Action<SimpleCard> _clickAction, Guid _cardGuid)
    {
        cardFace = Instantiate(_cardFaceTemplate, this.transform);
        clickAction = _clickAction;
        cardGuid = _cardGuid;
    }

    public void UpdateClickAction(Action<SimpleCard> _clickAction)
    {
        clickAction = _clickAction;
    }

    private void OnMouseDown()
    {
        this.isSelected = true;
        clickAction(this);
    }

    private void OnMouseUp()
    {
        this.isSelected = false;
        // Move card back to the hand
    }

    private void Update()
    {
        if (this.isSelected)
        {
            Debug.Log("Mouse button is clicked; Updating card position");
        }
    }


}
