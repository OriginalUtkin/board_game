using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;
public class PreparationMain : MonoBehaviour
{

    public CardCollection cardCollection;
    public PlayerSelection playerSelection;
    private PreparationLogic logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = new PreparationLogic();
        logic.initialize();
        cardCollection.SyncWithCardList(logic.collection);
        playerSelection.SyncWithCardList(logic.playerSelection);
    }

}
