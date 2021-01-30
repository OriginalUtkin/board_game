using UnityEngine;

public class Received : IState
{

    public SimpleCard card { get; set; }

    public Received(SimpleCard _card)
    {
        this.card = _card;
    }

    public void OnMouseDown()
    {
        Debug.Log("Log message");
    }

    public void OnMouseUp()
    {
        Debug.Log("Log message");
    }

    public void Update()
    {
        Debug.Log("Log message");

    }
}
