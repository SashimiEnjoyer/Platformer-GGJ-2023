using DG.Tweening;
using System.Collections;
using UnityEngine;


public class Opening : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public DialogueEntity DialogueEntity;

    private void Start()
    {
        StartCoroutine(DoStart());
    }

    IEnumerator DoStart()
    {
        yield return new WaitUntil(() => InGameTracker.instance != null);
        DialogueEntity.ExecuteInteractable();
        InGameTracker.instance.state = GameState.Dialogue;
    }

    public void FadeOutToPlay()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            InGameTracker.instance.state = GameState.Playing;
            gameObject.SetActive(false);
        });
    }
}
