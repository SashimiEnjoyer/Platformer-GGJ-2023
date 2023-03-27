using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameVersioning : MonoBehaviour
{
    [SerializeField] private TMP_Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = $"Version: {Application.version}";    
    }

}
