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

    bool IsPlayer1Active() { return currentPlayer == PlayerID.Player1; }

    public PreparationLogic GetCurrentPreparationLogic()
    {
        return IsPlayer1Active() ? player1Preparation : player2Preparation;
    }
    public void SetCurrentPreparationLogic(PreparationLogic logic)
    {
        if (IsPlayer1Active())
            player1Preparation = logic;
        else
            player2Preparation = logic;
    }

}
