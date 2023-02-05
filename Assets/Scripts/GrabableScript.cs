using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableScript : MonoBehaviour
{
    private bool isGrabbed = false;
    public void ExecuteInteractable(Transform player, bool isGrabbed)
    {
        transform.parent = isGrabbed ? player : null;
    }
}
