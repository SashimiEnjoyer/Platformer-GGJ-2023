using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalPlatform : MonoBehaviour
{

    [SerializeField] bool isHorizontal = true;
    [SerializeField] float verticalHeight;
    [SerializeField] float horizontalMove;
    [SerializeField] float moveTimer = 1;
    [SerializeField] Season platformSeason;

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
        else
        {
            if (isReset)
            {
                DoResetPlatfrom();
                isReset = false;
            }
        }


            switch (_season)
        {
            case Season.Winter:
                Debug.Log(_season.ToString());
                DoMovePlatfrom();
                break;
            case Season.Spring:
                Debug.Log(_season.ToString());
                DoMovePlatfrom();
                break;
            case Season.Summer:
                Debug.Log(_season.ToString());
                DoMovePlatfrom();
                break;
            case Season.Autumn:
                Debug.Log(_season.ToString());
                DoMovePlatfrom();
                break;
            default:
                break;
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
}
