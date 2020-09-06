using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject playSlot;

    public GameObject simpleCardPrefab;
    public GameObject[] facesPrefabs;

    private Vector3 HandPosition{
        get { return this.playerHand.transform.position; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.fillPlayerHand(hand_size: 4);
    }

    private void fillPlayerHand(int hand_size) {
         Debug.Log("filling player hand with cards");

         int offset = 2;

         int left_side = hand_size / 2;
         int right_side = hand_size - left_side;

         this.playerHand.transform.position = new Vector3(x: this.HandPosition.x - left_side * 1.5f, y: this.HandPosition.y, z: this.HandPosition.z);


         Vector3 cardPosition = new Vector3(x: this.HandPosition.x , y: this.HandPosition.y, z: this.HandPosition.z);

         for (int card_counter = 0; card_counter < hand_size; card_counter++){
             GameObject newCard = Instantiate(original: simpleCardPrefab, position: cardPosition, rotation: Quaternion.identity, parent: playerHand.transform);
             newCard.GetComponent<SimpleCard>().Setup(facesPrefabs[0], this);

             cardPosition.x += offset;
         }
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
