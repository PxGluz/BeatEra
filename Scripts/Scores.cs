using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text jungle;
    public Text egyptian;
    public Text medieval;
    private ScoreData data;
    public Image jstar1, jstar2, jstar3, estar1, estar2, estar3, mstar1, mstar2, mstar3;
    private int j, e, m;
    void Start()
    {
        data = ScoreSystem.ScoreLoad();
        if (data == null)
            j = e = m = 0;
        else
        {
            j = data.jungleScore;
            e = data.egyptianScore;
            m = data.medievalScore;
        }
        jstar1.color = new Color(0, 0, 0, 0);
        jstar2.color = new Color(0, 0, 0, 0);
        jstar3.color = new Color(0, 0, 0, 0);
        estar1.color = new Color(0, 0, 0, 0);
        estar2.color = new Color(0, 0, 0, 0);
        estar3.color = new Color(0, 0, 0, 0);
        mstar1.color = new Color(0, 0, 0, 0);
        mstar2.color = new Color(0, 0, 0, 0);
        mstar3.color = new Color(0, 0, 0, 0);
        if (j >= 1000)
            jstar1.color = new Color(1, 1, 1, 0);
        if (j >= 2000)
            jstar2.color = new Color(1, 1, 1, 0);
        if (j >= 3000)
            jstar3.color = new Color(1, 1, 1, 0);
        if (e >= 1000)
            estar1.color = new Color(1, 1, 1, 0);
        if (e >= 2000)
            estar2.color = new Color(1, 1, 1, 0);
        if (e >= 3000)
            estar3.color = new Color(1, 1, 1, 0);
        if (m >= 1000)
            mstar1.color = new Color(1, 1, 1, 0);
        if (m >= 2000)
            mstar2.color = new Color(1, 1, 1, 0);
        if (m >= 3000)
            mstar3.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (data == null)
        {
            jungle.text = "Highscore : N/A";
            egyptian.text = "Highscore : N/A";
            medieval.text = "Highscore : N/A";
        }
        else
        {
            if (data.jungleScore > 0)
                jungle.text = "Highscore : " + data.jungleScore.ToString();
            else
                jungle.text = "Highscore : N/A";
            if (data.egyptianScore > 0)
                egyptian.text = "Highscore : " + data.egyptianScore.ToString();
            else
                egyptian.text = "Highscore : N/A";
            if (data.medievalScore > 0)
                medieval.text = "Highscore : " + data.medievalScore.ToString();
            else
                medieval.text = "Highscore : N/A";
        }
    }
}
