using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreparationLogicHolder : MonoBehaviour
{

    public enum PlayerID
    {
        Player1,
        Player2
    };

    public PreparationLogic player1Preparation;
    public PreparationLogic player2Preparation;
    public PlayerID currentPlayer;

    public PreparationLogic GetCurrentPreparationLogic()
    {
        if (currentPlayer == PlayerID.Player1)
            return player1Preparation;
        return player2Preparation;
    }
    public void SetCurrentPreparationLogic(PreparationLogic logic)
    {
        if (currentPlayer == PlayerID.Player1)
            player1Preparation = logic;
        else
            player2Preparation = logic;
    }

}
