using UnityEngine;

public abstract class Card
{
    private Sprite cardFace;
    private Sprite cardBack;

    public Card(Sprite cardFace, Sprite cardBack)
    {
        this.cardFace = cardFace;
        this.cardBack = cardBack;
    }
}
