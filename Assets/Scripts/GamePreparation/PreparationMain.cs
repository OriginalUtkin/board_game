using System;
using System.Collections;
using System.Collections.Generic;
using BoardGame.Cards;
using BoardGame.Preparation;
using UnityEngine;
using UnityEngine.Events;
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

    PreparationLogicHolder GetHolder()
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
            Debug.Log("reuse existing PreparationLogicHolder");
            holder = obj.GetComponent<PreparationLogicHolder>();
        }
        if (holder.GetCurrentPreparationLogic() == null)
            holder.SetCurrentPreparationLogic(new PreparationLogic(_selectionTargetSize: playerSelectionSize));
        return holder;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start preparation main");

        preparationLogicHolder = GetHolder();
        DontDestroyOnLoad(preparationLogicHolder);

        logic = preparationLogicHolder.GetCurrentPreparationLogic();

        logic.selectionFullnessChanged = null;
        logic.selectionFullnessChanged += NextButtonFullnessChanged;

        SyncCards();
        ConfigureButtons();
    }

    static void EnableButton(Button button, string text, UnityAction onClick)
    {
        button.gameObject.SetActive(true);
        if (!(text is null))
            button.GetComponentInChildren<Text>().text = text;
        button.onClick.RemoveAllListeners();
        if (!(onClick is null))
            button.onClick.AddListener(onClick);
    }
    static void DisableButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    void ConfigureButtons()
    {
        if (preparationLogicHolder.currentPlayer == PreparationLogicHolder.PlayerID.Player1)
        {
            DisableButton(backButton);
            EnableButton(nextButton, "Player 2 - choose cards", Player2ChooseCardsButtonClicked);
        }
        else
        {
            EnableButton(backButton, "Player 1 - choose cards", Player1ChooseCardsButtonClicked);
            EnableButton(nextButton, "Start game!", StartGameClicked);
        }
        logic.EvaluateSelectionFullness();
    }

    void NextButtonFullnessChanged(bool isFull)
    {
        nextButton.interactable = isFull;
    }

    public void Player1ChooseCardsButtonClicked()
    {
        Debug.Log("player1ChooseCardsButtonClicked");

        preparationLogicHolder.currentPlayer = PreparationLogicHolder.PlayerID.Player1;
        SceneManager.LoadScene("CardSelection", LoadSceneMode.Single);

    }

    public void Player2ChooseCardsButtonClicked()
    {
        Debug.Log("player2ChooseCardsButtonClicked");

        preparationLogicHolder.currentPlayer = PreparationLogicHolder.PlayerID.Player2;
        SceneManager.LoadScene("CardSelection", LoadSceneMode.Single);

    }

    void SyncCards()
    {
        cardCollection.SyncWithCardList(logic.collection, cardsInScene);
        playerSelection.SyncWithCardList(logic.playerSelection, cardsInScene);
    }

    public void MoveCardToPlayerSelection(SimpleCard card)
    {
        logic.MoveCardToPlayerSelection(card.cardGuid);
        SyncCards();
    }

    public void MoveCardToCollection(SimpleCard card)
    {
        logic.MoveCardToCollection(card.cardGuid);
        SyncCards();
    }

    private void SavePlayerSelection()
    {
        logic.SaveToFile(System.IO.Path.Combine(gameObject.scene.path, "../playerSelection.xml"));
    }

    public void StartGameClicked()
    {
        Debug.Log("startGameClicked");
        this.SavePlayerSelection();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

}
