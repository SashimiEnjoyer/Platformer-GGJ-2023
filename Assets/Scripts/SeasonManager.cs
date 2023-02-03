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

    public void Update()
    {
        seasonTimer += (Time.deltaTime * timerMultiplier);
        seasonTimer %= seasonCooldown;

        testtimer.text = seasonTimer.ToString();


        if (seasonTimer > 0 && seasonTimer < 3)
            onSeasonChange?.Invoke(Season.Winter);
        else if (seasonTimer > 3 && seasonTimer < 6)
            onSeasonChange?.Invoke(Season.Spring);
        if (seasonTimer > 6 && seasonTimer < 9)
            onSeasonChange?.Invoke(Season.Summer);
        if (seasonTimer > 9 && seasonTimer < 12)
            onSeasonChange?.Invoke(Season.Autumn);
    }

    void ChangeSeasonUI(Season season)
    {
        seasonText.text = season.ToString();
    }
}
