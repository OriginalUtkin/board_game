using UnityEngine;

public class Movable : IState
{

    public SimpleCard card { get; set; }
    public Vector3 startingPosition { get; set; }

    public Movable(SimpleCard _card)
    {
        this.card = _card;
    }
    public void OnMouseDown()
    {
        this.card.isSelected = true;
        startingPosition = this.card.transform.position;
    }

    public void OnMouseUp()
    {
        Debug.Log("Mouse button is released; Do not move card " + this.card.cardGuid);

        this.card.isSelected = false;
        IInteractable? collisionObject = this.GetColissionObject();

        if (collisionObject == null || !collisionObject.IsReceivable(this.card))
            this.card.ResetPosition(startingPosition);
        else
        {
            this.card.state = new Received(this.card);
            collisionObject.ReceiveObject(this.card);
        }
    }

    public void Update()
    {
        if (this.card.isSelected)
        {
            // Debug.Log("Mouse button is clicked; Updating card position " + this.cardGuid);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.card.transform.position = new Vector3(cursorPosition.x, cursorPosition.y, -5.0f);

            // Debug.Log("Mouse position " + Input.mousePosition + " Card position " + this.transform.position);
        }
    }

    private IInteractable? GetColissionObject()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(
            this.card.transform.position,
            this.card.transform.TransformDirection(Vector3.forward),
            out hit,
            Mathf.Infinity,
            Physics.DefaultRaycastLayers
        );

        Debug.DrawRay(this.card.transform.position, this.card.transform.TransformDirection(Vector3.forward) * 20.0f, isHit ? Color.yellow : Color.red);

        if (!isHit)
            return null;

        IInteractable collisionObject = (IInteractable)hit.transform.gameObject.GetComponent(typeof(IInteractable));

        return collisionObject;
    }
}
