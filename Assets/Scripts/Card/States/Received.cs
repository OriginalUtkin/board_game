using UnityEngine;

<<<<<<< HEAD
public class Received : IState
{

    public SimpleCard card { get; set; }

    public Received(SimpleCard _card)
    {
=======
public class Received : IState {
    
    public SimpleCard card {get; set;}

    public Received(SimpleCard _card){
>>>>>>> Implement possible card states
        this.card = _card;
    }

    public void OnMouseDown()
    {
        Debug.Log("Log message");
    }

    public void OnMouseUp()
    {
<<<<<<< HEAD
        Debug.Log("Log message");
=======
        Debug.Log("Log message");    
>>>>>>> Implement possible card states
    }

    public void Update()
    {
<<<<<<< HEAD
        // Debug.Log("Log message");

    }
}
=======
        Debug.Log("Log message");

    }
}
>>>>>>> Implement possible card states
