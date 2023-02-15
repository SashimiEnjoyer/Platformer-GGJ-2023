using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableScript : MonoBehaviour
{
    private bool isGrabbed = false;
    [SerializeField] GameObject popup;
    Vector2 posCache;

    private void Start()
    {
        SeasonManager.instance.onSeasonChange += ShowPopupUI; 
    }

    private void OnDisable()
    {
        SeasonManager.instance.onSeasonChange -= ShowPopupUI;
    }

    private void Update()
    {
        if (!popup.activeInHierarchy)
            return;

        posCache = popup.transform.position;
        posCache.x = transform.position.x;
        popup.transform.position = posCache;
    }

    public void ExecuteInteractable(Transform player, bool isGrabbed)
    {
        transform.parent = isGrabbed ? player : null;
    }

    void ShowPopupUI(Season season)
    {
        switch (season)
        {

            case Season.Spring:
                popup.SetActive(true);
                break;
            case Season.Summer:
                popup.SetActive(false);
                break;
            default:
                break;
        }
    }
}
