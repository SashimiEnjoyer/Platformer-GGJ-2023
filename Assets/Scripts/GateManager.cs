using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class GateManager : MonoBehaviour
{
    public float verticalHeight;
    public float moveTimer;

    public UnityEvent onFinishedMove;
    public GameObject cameraPreview;
    public bool isMoved = false;

    public void ExecuteMove()
    {
        if(isMoved)
            return;

        cameraPreview.SetActive(true);
        DOTween.To(() => transform.position, x => transform.position = x, new Vector3(transform.position.x, transform.position.y + verticalHeight), moveTimer).
            SetEase(Ease.InSine).
            OnComplete(()=>
            {
                isMoved = true;
                cameraPreview.SetActive(false);
                onFinishedMove?.Invoke();
            });

    }

}
