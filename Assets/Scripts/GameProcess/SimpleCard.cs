using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCard : MonoBehaviour
{
    GameObject cardFace;
    Action<SimpleCard> clickAction;
    public Guid cardGuid;

    public void Setup(GameObject _cardFaceTemplate, Action<SimpleCard> _clickAction, Guid _cardGuid)
    {
        cardFace = Instantiate(_cardFaceTemplate, transform);
        clickAction = _clickAction;
        cardGuid = _cardGuid;
    }

    public void UpdateClickAction(Action<SimpleCard> _clickAction)
    {
        clickAction = _clickAction;
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse down");
        clickAction(this);
    }
}
