using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text value;
    public GameObject gameManager;
    private Manager manager;
    private bool ok = true;
    public GameObject parent;
    private bool milbei;

    void Start()
    {
        manager = gameManager.GetComponent<Manager>();
        milbei = false;
    }

    
    void Update()
    {
        if (manager.gm == false)
            value.text = manager.score.ToString();
        else
        {
            if (manager.highscore == manager.score2)
            {
                milbei = true;
                manager.score3 = manager.score2;
            }
            if (milbei == true)
                value.text = manager.score3.ToString();
            else
                value.text = manager.score2.ToString();
        }
        if (manager.gm == true && ok==true)
        {
            value.fontSize = 65;
            this.transform.position = new Vector3(parent.transform.position.x + 150f, parent.transform.position.y, this.transform.position.z);
        }
    }
}
