using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPressedInteraction : MonoBehaviour
{
    [SerializeField] UnityEvent onPressedButton;
    bool isPressed = false;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isPressed)
        {
            onPressedButton?.Invoke();
            isPressed = true;
        }
    }
}
