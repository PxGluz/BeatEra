using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnteringAnimation : MonoBehaviour
{
    private float timeToWait = 3f;
    public Spawner spwn;
    public Manager mng;
    public Score scr;
    public Timer tim;
    public EnteringAnimation ea;
    private GameObject player;
    public GameObject gameManager;
    private Manager manager;
    private PlayerMovement pmv;
    private Animator anim;
    public Text score, timer;
    private string nam;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        timeToWait += Time.time;
        score.color = new Color(score.color.r, score.color.g, score.color.b, 0f);
        timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, 0f);
    }

    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nam);
        player = GameObject.Find(nam);
        //player = GameObject.Find("Treasure_Hunter(Clone)");
        pmv = player.GetComponent<PlayerMovement>();
        anim = player.GetComponent<Animator>();
        if (timeToWait < Time.time)
        {
            if(score.color.a < 1f)
            {
                score.color = new Color(score.color.r, score.color.g, score.color.b, score.color.a + 0.05f);
                timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, timer.color.a + 0.05f);
            }
            else
                ea.enabled = false;
            spwn.enabled = true;
            scr.enabled = true;
            tim.enabled = true;
            mng.entered = true;
        }
    }
}
