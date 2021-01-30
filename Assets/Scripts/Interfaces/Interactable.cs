using UnityEngine;
public interface IInteractable
{
    void ReceiveObject(MonoBehaviour obj);
    bool IsReceivable();
}
