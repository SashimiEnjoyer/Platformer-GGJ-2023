using UnityEngine;
using UnityEngine.Events;

public class ButtonPressedInteraction : MonoBehaviour
{
    [SerializeField] GameObject popUp;
    [SerializeField] UnityEvent onPressedButton;
    bool isPressed = false;
    bool isTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouched = false;
        }
    }

    private void Update()
    {
        if (isPressed)
            return;

        popUp.SetActive(isTouched);
        
        if (!isTouched)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            onPressedButton?.Invoke();
            isPressed = true;
        }
    }

}
