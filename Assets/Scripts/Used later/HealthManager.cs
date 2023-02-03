using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Text health;
    public static int healthAmmount = 5;
    public int healthReset = 5;
    public GameObject displayStats;
    void Start()
    {
        health.text = PlayerPrefs.GetInt("Health",5).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = " x " + healthAmmount.ToString();
        PlayerPrefs.SetInt("Health", healthAmmount);
    }
}
