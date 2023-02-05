using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour, IInteractable
{
    public UnityEvent interactionEvent;

    public void  ExecuteInteractable()
    {
        interactionEvent?.Invoke();
    }
}
