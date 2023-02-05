using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void ChangeScene3(){
        SceneManager.LoadScene("Scene 1");
    }   
    
    public void ChangeScene4(){
        SceneManager.LoadScene("Scene 4");
    }   
}



public class TriggerButton : MonoBehaviour
{
    public Button targetButton;

    private void Start()
    {
        targetButton.gameObject.SetActive(false);
    }

    public void OnTriggerButtonClick()
    {
        targetButton.gameObject.SetActive(true);
    }
}