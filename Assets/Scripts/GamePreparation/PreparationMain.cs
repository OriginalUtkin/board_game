using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardGame.Preparation;
using BoardGame.Cards;
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
