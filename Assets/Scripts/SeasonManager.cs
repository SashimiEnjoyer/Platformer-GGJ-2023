using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum Season { Winter, Spring, Summer, Autumn}

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;
    public TMP_Text testtimer;
    public TMP_Text seasonText;
    
    private Season _season;
    private float seasonTimer;
    private float divValue;
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
        onSeasonChange += ChangeSeasonUI;
    }

    private void OnDisable()
    {
        onSeasonChange -= ChangeSeasonUI;
    }

    private void Start()
    {
        divValue = seasonCooldown / Enum.GetNames(typeof(Season)).Length;
    }

    public void Update()
    {
        seasonTimer += (Time.deltaTime * timerMultiplier);
        seasonTimer %= seasonCooldown;

        testtimer.text = seasonTimer.ToString();

        onSeasonChange?.Invoke((Season)(seasonTimer / divValue));
    }

    void ChangeSeasonUI(Season season)
    {
        seasonText.text = season.ToString();
    }
}
