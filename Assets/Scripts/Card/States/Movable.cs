using UnityEngine;


<<<<<<< HEAD
public class Movable : IState
{

    public SimpleCard card { get; set; }
    public Vector3 startingPosition { get; set; }

    public Movable(SimpleCard _card)
    {
=======
public class Movable: IState{

    public SimpleCard card {get; set;}
    public Movable(SimpleCard _card){
>>>>>>> Implement possible card states
        this.card = _card;
    }
    public void OnMouseDown()
    {
        this.card.isSelected = true;
<<<<<<< HEAD
        startingPosition = this.card.transform.position;
=======
>>>>>>> Implement possible card states
    }

    public void OnMouseUp()
    {
        Debug.Log("Mouse button is released; Do not move card " + this.card.cardGuid);

        this.card.isSelected = false;
        IInteractable? collisionObject = this.GetColissionObject();

<<<<<<< HEAD
        if (collisionObject == null || !collisionObject.IsReceivable(this.card))
            this.card.ResetPosition(startingPosition);
        else
        {
            this.card.state = new Received(this.card);
            collisionObject.ReceiveObject(this.card);
        }
=======
        if (collisionObject == null || !collisionObject.IsReceivable())
            this.card.ResetPosition();
        else{
            collisionObject.ReceiveObject(this.card);
            this.card.state = new Received(this.card);       
        }   
>>>>>>> Implement possible card states
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

        IInteractable collisionObject = (IInteractable)hit.collider.GetComponent(hit.collider.name);

        return collisionObject;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> Implement possible card states
