using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GateManager : MonoBehaviour
{
    public float verticalHeight;
    public float moveTimer;

    public UnityEvent onFinishedMove;
    public GameObject cameraPreview;
    public bool isMoved = false;

    IEnumerator DoMovePlatfrom()
    {
        cameraPreview.SetActive(true);
        transform.DOMove(new Vector2(transform.position.x, transform.position.y + verticalHeight), moveTimer); ;
        yield return new WaitForSeconds(4f);
        cameraPreview.SetActive(false);
        onFinishedMove?.Invoke();
    }

    public void ExecuteMove()
    {
        if(isMoved)
            return;

        isMoved = true;

        StartCoroutine(DoMovePlatfrom());
    }

}
