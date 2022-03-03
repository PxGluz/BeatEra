using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueCost : MonoBehaviour
{
    public GameObject gameManager;
    private Manager manager;
    public Text cost;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        cost.text = manager.price.ToString() + " gold";
    }
}
