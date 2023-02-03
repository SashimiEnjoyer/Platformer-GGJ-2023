﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    float currentTime = 0f, speedTime = 1f;
    public Text timerTxt;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 365f;
    }

    // Update is called once per frame
    void Update()
    {
        timerTxt.text = currentTime.ToString("F0");
        timerCounter();

        if (Input.GetKeyDown("f"))
        {
            print("space key was pressed");
        }
    }


    public void timerCounter()
    {
        currentTime -= speedTime * Time.deltaTime;
    }

    public void SkipTime()
    {
        speedTime = 3f;
    }
    public void normalTime()
    {
        speedTime = 1f;
    }
}
