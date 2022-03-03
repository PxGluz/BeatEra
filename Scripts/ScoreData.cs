using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int jungleScore , egyptianScore , medievalScore;

    public ScoreData (Manager manager)
    {
        if (manager.levelIndex == 1)
        {
            jungleScore = manager.score;
            egyptianScore = manager.e;
            medievalScore = manager.m;
        }
        if (manager.levelIndex == 2)
        {
            egyptianScore = manager.score;
            jungleScore = manager.j;
            medievalScore = manager.m;
        }
        if (manager.levelIndex == 3)
        {
            medievalScore = manager.score;
            egyptianScore = manager.e;
            jungleScore = manager.j;
        }
    }
}
