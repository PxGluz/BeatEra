using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject gameManager;
    private Manager manager;
    public Button but;

    void OnEnable()
    {
        but.onClick.AddListener(ResPause);
    }

    void ResPause()
    {
        manager.pause = false;
        //Debug.Log("ma pis");
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
    }

    void Update()
    {
        
    }
}
