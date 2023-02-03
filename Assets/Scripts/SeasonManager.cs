using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season { Winter, Spring, Summer, Autumn}

public class SeasonManager : MonoBehaviour
{

    public static SeasonManager instance;
    public Season season;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
