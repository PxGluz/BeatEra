using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueTimer : MonoBehaviour
{
    private int y;
    public int totalTime = 3;
    public GameObject gameManager;
    private Manager manager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        y = (int)Time.time;
    }

    void Update()
    {
        if (totalTime > 0)
        {
            if (y != (int)Time.time)
                totalTime--;
            y = (int)Time.time;
        }
        else
        {
            manager.again = false;
            if(manager.alive == false)
                manager.gm = true;
            Destroy(this.gameObject);
        }
    }
}
