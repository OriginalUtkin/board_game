using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardGame.Preparation;
public class PreparationMain : MonoBehaviour
{

    public CardCollection cardCollection;
    private PreparationLogic logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = new PreparationLogic();
        cardCollection.SyncWithCardList(logic.collection);
    }

}
