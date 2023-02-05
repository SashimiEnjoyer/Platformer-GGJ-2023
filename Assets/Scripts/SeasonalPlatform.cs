using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalPlatform : MonoBehaviour
{
    public bool playerStick = false;

    [SerializeField] bool isHorizontal = true;
    [SerializeField] float verticalHeight;
    [SerializeField] float horizontalMove;
    [SerializeField] float moveTimer = 1;
    [SerializeField] Season platformSeason;
    [SerializeField] Season offPlatformSeason;

    bool isReset = false;

    private void Start()
    {
        SeasonManager.instance.onSeasonChange += ChangeSeason;
    }

    private void OnDisable()
    {
        SeasonManager.instance.onSeasonChange -= ChangeSeason;
    }

    void ChangeSeason(Season _season)
    {
        if (_season == platformSeason)
        {
            DoMovePlatfrom();
            isReset = true;
        }
        else if(_season == offPlatformSeason)
        {
            DoResetPlatfrom();

            if (isReset)
            {
                isReset = false;
            }
        }

    }

    void DoMovePlatfrom()
    {
        transform.DOMove((isHorizontal? new Vector2(transform.position.x + horizontalMove, transform.position.y ) : 
            new Vector2(transform.position.x, transform.position.y + verticalHeight)), moveTimer);
    }


    void DoResetPlatfrom()
    {
        transform.DOMove((isHorizontal ? new Vector2(transform.position.x + -horizontalMove, transform.position.y) :
            new Vector2(transform.position.x, transform.position.y + -verticalHeight)), moveTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerStick)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerStick)
        {
            collision.transform.parent = null;
        }
    }
}
