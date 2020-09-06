using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playSlot;

    public GameObject simpleCardPrefab;
    public GameObject[] facesPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        CreateNewCard();
    }

    private void CreateNewCard() {
        Debug.Log("creating new card");

        GameObject newCard = Instantiate(simpleCardPrefab, playerHand.transform);
        newCard.GetComponent<SimpleCard>().Setup(facesPrefabs[0], this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCard(SimpleCard card) {
        Debug.Log("playing card");
        Debug.Log(card);
        card.transform.SetParent(playSlot.transform);
        card.transform.position = Vector3.zero;
    }
}
