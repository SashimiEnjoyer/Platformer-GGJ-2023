using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public enum Season { Winter, Spring, Summer, Autumn}


public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;
    public TMP_Text testtimer;
    public TMP_Text seasonText;

    [SerializeField] private GameObject[] tilemaps;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Sprite[] seasonParticleSprite;
    [SerializeField] private PostProcessVolume[] volume;
    
    [SerializeField]private Season _season;
    private Season lastSeason = Season.Autumn;
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

            lastSeason = _season;
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
        onSeasonChange += ChangeParticle;
        onSeasonChange += ChangePostProVolume;
    }

    private void OnDisable()
    {
        onSeasonChange -= ChangeTilemaps;
        onSeasonChange -= ChangeSeasonUI;
        onSeasonChange -= ChangeParticle;
        onSeasonChange -= ChangePostProVolume;
    }

    private void Start()
    {
        season = Season.Winter;
    }

    public void Update()
    {

        if (seasonTimer > 0 && seasonTimer < 12)
            season = Season.Winter;
        else if (seasonTimer > 12 && seasonTimer < 24)
            season = Season.Spring;
        else if (seasonTimer > 24 && seasonTimer < 36)
            season = Season.Summer;
        else if (seasonTimer > 36 && seasonTimer < 48)
            season = Season.Autumn;


        if (InGameTracker.instance.state == GameState.Playing)
        {
            seasonTimer += (Time.deltaTime * timerMultiplier);
            testtimer.text = seasonTimer.ToString("f1");
        }

        seasonTimer %= seasonCooldown;


    }

    void ChangeSeasonUI(Season season)
    {
        seasonText.text = season.ToString();
    }

    void ChangeParticle(Season season)
    {
        particle.textureSheetAnimation.SetSprite(0, seasonParticleSprite[(int)season]);

        if(season == Season.Summer)
        {
            particle.Stop();
        }
        else
        {
            particle.Play();
        }
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

    void ChangePostProVolume(Season season)
    {
        int lastSeason = ((int)season - 1);
        lastSeason += (lastSeason < 0 ? 4 : 0);
        DOTween.To(() => volume[(int)season].weight, x => volume[(int)season].weight = x, 1f, 1);
        DOTween.To(() => volume[lastSeason].weight, x => volume[lastSeason].weight = x, 0, 1);
    }
}
