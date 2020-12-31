using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimpleCard : MonoBehaviour
{
    GameObject cardFace;
    public Guid cardGuid;
    public bool isSelected { get; private set; } = false;
    public bool isPlacedOnBattlefield { get; private set; } = false;

    public void Setup(GameObject _cardFaceTemplate, Guid _cardGuid)
    {
        cardFace = Instantiate(_cardFaceTemplate, this.transform);
        cardGuid = _cardGuid;
    }

    private void OnMouseDown()
    {
        this.isSelected = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse button is released; Do not move card " + this.cardGuid);

        this.isSelected = false;
        IInteractable? collisionObject = this.GetColissionObject();

        if (collisionObject == null)
            this.PutCardToHand();
        else
            collisionObject.RecieveObject(this);
    }

    private void Update()
    {
        if (this.isSelected)
        {
            Debug.Log("Mouse button is clicked; Updating card position " + this.cardGuid);

            Vector3 coursorePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(coursorePosition.x, coursorePosition.y, -5.0f);

            Debug.Log("Mouse position " + Input.mousePosition + " Card position " + this.transform.position);
        }
    }

    private IInteractable? GetColissionObject()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(
            this.transform.position,
            transform.TransformDirection(Vector3.forward),
            out hit,
            Mathf.Infinity,
            Physics.DefaultRaycastLayers
        );

        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 20.0f, isHit ? Color.yellow : Color.red);

        if (hit.collider == null)
            return null;

        IInteractable collisionObject = (IInteractable)hit.collider.GetComponent(hit.collider.name);

        return collisionObject;
    }

    private void PutCardToHand()
    {

    }
}
