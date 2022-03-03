using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button button;
    private bool ok = false, ok2 = false;
    private float timeToWait = 1f; 

    public GameObject fadeOut;

    void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Exit);
    }
    
    void Exit()
    {
        ok = true;
        timeToWait += Time.time;
        if (ok2 == true)
        {
            Application.Quit();
        }
        fadeOut.SetActive(true);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(ok == true)
        {
            if (Time.time > timeToWait)
            {
                ok2 = true;
                Exit();
            }
        }
    }
}
