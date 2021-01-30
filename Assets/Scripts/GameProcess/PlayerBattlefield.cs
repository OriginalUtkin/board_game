using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerBattlefield : MonoBehaviour, IInteractable
{
    public GameObject[] playerSlots;
    private const int Capacity = 5;
    private const int BattlefieldCenter = PlayerBattlefield.Capacity / 2;
    private int AvailableSpots { get; set; } = PlayerBattlefield.Capacity;
    private bool IsEmpty => (this.AvailableSpots == PlayerBattlefield.Capacity);

    public bool IsReceivable(MonoBehaviour obj)
    {
        return this.AvailableSpots > 0;
    }
    public void ReceiveObject(MonoBehaviour obj)
    {
        Debug.Log("Object is received");
        int cardPosition;

        if (this.IsEmpty)
            cardPosition = PlayerBattlefield.BattlefieldCenter;
        else
            cardPosition = this.CalculateCardPosition();

        obj.transform.position = this.playerSlots[cardPosition].transform.position;

        this.AvailableSpots -= 1;
    }

    private int CalculateCardPosition()
    {
        Debug.Log("Calculating spot coordinates");

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<float> distances = this.playerSlots.Select(
                slot => Vector3.Distance(slot.transform.position, cursorPosition)
            ).ToList();
        int closestIndex = distances.IndexOf(distances.Min());

        Debug.Log("Closest slot is " + closestIndex);

        return closestIndex;
    }
}
