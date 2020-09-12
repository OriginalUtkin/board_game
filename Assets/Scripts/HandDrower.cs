using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDrower : MonoBehaviour
{
    public readonly static int handLimit = 4;

    public GameObject simpleCardPrefab;
    public GameObject[] facesPrefabs;

    public static Vector3 calculateStartHandPosition(Vector3 handPositionCoordinate)
    {
        int left_side = HandDrower.handLimit / 2;
        return handPositionCoordinate + Vector3.left * left_side * 1.5f;
    }


    public void fillStartHand(GameObject cardParent, Vector3 cardPosition, Main _mainScript)
    {
        int offset = 2;

        for (int card_counter = 0; card_counter < HandDrower.handLimit; card_counter++)
        {
            GameObject newCard = Instantiate(original: simpleCardPrefab, position: cardPosition, rotation: Quaternion.identity, parent: cardParent.transform);
            newCard.GetComponent<SimpleCard>().Setup(facesPrefabs[0], _mainScript.PlayCard, Guid.NewGuid());  // TODO sync with GameLogic instead

            cardPosition.x += offset;
        }
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
