using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum Season { Winter, Spring, Summer, Autumn}


public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;
    public TMP_Text testtimer;
    public TMP_Text seasonText;

    [SerializeField] private GameObject[] tilemaps;
    
    [SerializeField]private Season _season;
    private float seasonTimer;
    public float timerMultiplier = 1;
    [SerializeField] private float seasonCooldown;
    public UnityAction<Season> onSeasonChange;
    public Season season
    {
        set
        {
            if (value == _season)
                return;

            _season = value;
            seasonTimer = seasonCooldown * ((float)_season / Enum.GetNames(typeof(Season)).Length);
            onSeasonChange?.Invoke(value);
            Debug.Log("Set Season: " + _season);
        }

        get
        {
            return _season;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        onSeasonChange += ChangeTilemaps;
        onSeasonChange += ChangeSeasonUI;
    }

    private void OnDisable()
    {
        onSeasonChange -= ChangeTilemaps;
        onSeasonChange -= ChangeSeasonUI;
    }

    private void Start()
    {
        season = Season.Winter;
    }

    public void Update()
    {

        if (seasonTimer > 0 && seasonTimer < 4)
            season = Season.Winter;
        else if (seasonTimer > 4 && seasonTimer < 8)
            season = Season.Spring;
        else if (seasonTimer > 8 && seasonTimer < 12)
            season = Season.Summer;
        else if (seasonTimer > 12 && seasonTimer < 16)
            season = Season.Autumn;


        seasonTimer += (Time.deltaTime * timerMultiplier);
        seasonTimer %= seasonCooldown;

        testtimer.text = seasonTimer.ToString("f2");


    }

    void ChangeSeasonUI(Season season)
    {
        seasonText.text = season.ToString();
    }

    [ContextMenu("Test Change Season")]
    public void ChangeTilemaps(Season season)
    {
        foreach (var tilemap in tilemaps)
        {
            tilemap.SetActive(false);
        }

        tilemaps[(int)season].SetActive(true);
    }
}
