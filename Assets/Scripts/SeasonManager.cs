using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum Season { Winter, Spring, Summer, Autumn}

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;
    public TMP_Text testtimer;
    
    private Season _season;
    private float seasonTimer;
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

    public void Update()
    {
        seasonTimer += Time.deltaTime;
        seasonTimer %= seasonCooldown;

        testtimer.text = seasonTimer.ToString();

    }
}
