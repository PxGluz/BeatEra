using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Spawner spawner;
    public Text clock;
    public int timp,sec,min,totalTime;
    private string dots;
    public GameObject gameManager;
    private Manager manager;
    private int y;
    private GameObject player;
    private PlayerMovement pm;
    public GameObject portal;
    private bool ok = false;
    private string nam;

    void Start()
    {
        y = 0;
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        spawner = gameManager.GetComponent<Spawner>();
        nam = manager.charactersArray[manager.selection].ToString();
        char[] name2 = new char[50];
        int i = 0;
        while (nam[i] != '(')
            i++;
        for (int j = 0; j < i - 1; j++)
            name2[j] = nam[j];
        name2[i - 1] = '(';
        name2[i] = 'C';
        name2[i + 1] = 'l';
        name2[i + 2] = 'o';
        name2[i + 3] = 'n';
        name2[i + 4] = 'e';
        name2[i + 5] = ')';
        nam = new string(name2);
    }

    void FixedUpdate()
    {
        spawner.t = totalTime;
        if (manager.again == false)
        {
            if(manager.bonusTime == true)
            {
                totalTime += manager.bonus;
                manager.bonusTime = false;
            }
            timp = (int)Time.time;
            if (y != timp && totalTime > 0)
                totalTime--;
            sec = totalTime % 60;
            min = totalTime / 60;
            if (totalTime == 0 && manager.addedAmount > 0)
            {
                manager.addedAmount = 0;
                player = GameObject.Find(nam);
                pm = player.GetComponent<PlayerMovement>();
                Vector3 pos = new Vector3(player.transform.position.x, 4.5f, player.transform.position.z);
                if(ok == false && player.name != "Skeleton(Clone)" && player.name != "DJ(Clone)" && player.name != "Witch(Clone)")
                {
                    Object.Instantiate(portal, pos, new Quaternion(0, 0, 0, 0));
                    portal = GameObject.Find("Portal(Clone)");
                    portal.transform.Rotate(-90f, 0, 0);
                }
                ok = true;
                pm.waitTime = Time.time + 1f;
                pm.dead = true;
                pm.Die();
            }
            if (sec < 10)
            {
                dots = ":0";
            }
            else
                dots = ":";
            clock.text = "0" + min.ToString() + dots + sec.ToString();
            y = timp;
        }
    }
}
