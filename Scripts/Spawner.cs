using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int t, coinChance;
    public GameObject player;
    public PlayerMovement pm;
    private GameObject gameManager;
    private Manager manager;
    private float timeToWait = 1f;
    public GameObject shrink;
    public GameObject addtime;
    public GameObject snowflake;
    public GameObject coin;
    public int[,] b;
    private bool check;
    private string nam;

    void OnEnable()
    {
        timeToWait += Time.time;
    }

    private int Fx(int x)
    {
        if (x < 5)
            return 80 - (x * x * x) / 3;
        if (x < 60)
            return (-12 / 11) * x + 720 / 11;
        return 0;
    }

    private int Fy(float x)
    {
        if (x > 2)
            return (int)(205 - 65 * x);
        return 75;
    }

    void Awake()
    {
        gameManager = this.gameObject;
        manager = gameManager.GetComponent<Manager>();
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
        player = GameObject.Find(nam);
        pm = player.GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        b = new int[10, 10];
        b[pm.stepsx + 4, pm.stepsz + 4] = 1;
        if (manager.spark != check && manager.spark == true && timeToWait < Time.time && manager.hedeadtho == false)
        {
            int chance = manager.mapSize * 10;
            int aux = Random.Range(1, 100);
            int x, y;
            Vector3 p;
            Quaternion r;
            if (manager.mapSize > 1 && aux <= chance && manager.score % manager.scoreOfChange != manager.scoreOfChange - manager.addedAmount)
            {
                do
                {
                    x = Random.Range(4 - manager.mapSize + 1, 4 + manager.mapSize - 1);
                    y = Random.Range(4 - manager.mapSize + 1, 4 + manager.mapSize - 1);
                } while (b[x, y] == 1);
                b[x, y] = 1;
                p = new Vector3((x - 4) * 1.2f, 3.5f, (y - 4) * 1.2f);
                r = new Quaternion(0, 45f, 0, 90f);
                Object.Instantiate(shrink, p, r);
            }
            chance = Fx(t);
            aux = Random.Range(0, 100);
            if (aux < chance)
            {
                do
                {
                    x = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                    y = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                } while (b[x, y] == 1);
                b[x, y] = 1;
                p = new Vector3((x - 4) * 1.2f, 3.7f, (y - 4) * 1.2f);
                r = new Quaternion(0, 45f, 0, 90f);
                Object.Instantiate(addtime, p, r);
            }
            chance = Fy(manager.delay);
            aux = Random.Range(0, 100);
            if (aux < chance)
            {
                do
                {
                    x = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                    y = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                } while (b[x, y] == 1);
                b[x, y] = 1;
                p = new Vector3((x - 4) * 1.2f, 3.7f, (y - 4) * 1.2f);
                r = new Quaternion(0, 45f, 0, 90f);
                Object.Instantiate(snowflake, p, r);
            }
            timeToWait = Time.time + 1f;
            chance = coinChance;
            aux = Random.Range(0, 100);
            if(aux < chance)
            {
                do
                {
                    x = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                    y = Random.Range(4 - manager.mapSize, 4 + manager.mapSize);
                } while (b[x, y] == 1);
                b[x, y] = 1;
                p = new Vector3((x - 4) * 1.2f, 3.7f, (y - 4) * 1.2f);
                r = new Quaternion(0, 45f, 0, 90f);
                Object.Instantiate(coin, p, r);
            }
        }
        check = manager.spark;
    }
}
