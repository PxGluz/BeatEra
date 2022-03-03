using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public GameObject gameManager;
    private Manager manager;
    public Text score;

    void Awake()
    {
        manager = gameManager.GetComponent<Manager>();
    }

    void Update()
    {
        score.text = manager.highscore.ToString();
    }
}
