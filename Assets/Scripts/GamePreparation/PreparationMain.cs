using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreparationMain : MonoBehaviour
{

    public int playerSelectionSize = 5;

    public CardCollection cardCollection;
    public PlayerSelection playerSelection;
    public Button backButton;
    public Button nextButton;

    public Dictionary<Guid, GameObject> cardsInScene = new Dictionary<Guid, GameObject>();

    public PreparationLogicHolder preparationLogicHolderPrefab;

    private PreparationLogic logic;
    private PreparationLogicHolder preparationLogicHolder;

    // Start is called before the first frame update

    PreparationLogicHolder GetOrCreateHolder()
    {
        GameObject obj = GameObject.Find("/PreparationLogicHolder");
        PreparationLogicHolder holder;
        if (obj is null)
        {
            Debug.Log("create new PreparationLogicHolder");

            holder = Instantiate(preparationLogicHolderPrefab, null);
            holder.name = "PreparationLogicHolder";
            holder.currentPlayer = PreparationLogicHolder.PlayerID.Player1;
            obj = holder.gameObject;
        }
        else
        {
            Debug.Log("reuse PreparationLogicHolder");
            holder = obj.GetComponent<PreparationLogicHolder>();
        }
        if (holder.GetCurrentPreparationLogic() == null)
            holder.SetCurrentPreparationLogic(new PreparationLogic(_selectionTargetSize: playerSelectionSize));
        return holder;
    }

    void Start()
    {
        Debug.Log("start preparation main");

        preparationLogicHolder = GetOrCreateHolder();
        DontDestroyOnLoad(preparationLogicHolder);

        logic = preparationLogicHolder.GetCurrentPreparationLogic();

        logic.selectionFullnessChanged = null;
        logic.selectionFullnessChanged += nextButtonFullnessChanged;

        syncCards();
        configureButtons();
    }

    void configureButtons()
    {
        if (preparationLogicHolder.currentPlayer == PreparationLogicHolder.PlayerID.Player1)
        {
            nextButton.GetComponentInChildren<Text>().text = "Player 2 - choose cards";
            nextButton.onClick.AddListener(player2ChooseCardsButtonClicked);

            backButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.GetComponentInChildren<Text>().text = "Start game!";
            nextButton.onClick.AddListener(startGameClicked);

            backButton.gameObject.SetActive(true);
            backButton.GetComponentInChildren<Text>().text = "Player 1 - choose cards";
            backButton.onClick.AddListener(player1ChooseCardsButtonClicked);
        }
        logic.evaluateSelectionFullness();
    }

    void nextButtonFullnessChanged(bool isFull)
    {
        nextButton.interactable = isFull;
    }

    public void player1ChooseCardsButtonClicked()
    {
        Debug.Log("player1ChooseCardsButtonClicked");

        preparationLogicHolder.currentPlayer = PreparationLogicHolder.PlayerID.Player1;
        SceneManager.LoadScene("CardSelection", LoadSceneMode.Single);

    }

    public void player2ChooseCardsButtonClicked()
    {
        Debug.Log("player2ChooseCardsButtonClicked");

        preparationLogicHolder.currentPlayer = PreparationLogicHolder.PlayerID.Player2;
        SceneManager.LoadScene("CardSelection", LoadSceneMode.Single);

    }

    void syncCards()
    {
        cardCollection.SyncWithCardList(logic.collection, cardsInScene);
        playerSelection.SyncWithCardList(logic.playerSelection, cardsInScene);
    }

    public void moveCardToPlayerSelection(SimpleCard card)
    {
        logic.moveCardToPlayerSelection(card.cardGuid);
        syncCards();
    }

    public void moveCardToCollection(SimpleCard card)
    {
        logic.moveCardToCollection(card.cardGuid);
        syncCards();
    }

    public void startGameClicked()
    {
        Debug.Log("startGameClicked");
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

}
