using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private Spawner spawn;
    private ContinueTimer ct;
    public GameObject pillar;
    public GameObject parent;
    public GameObject player;
    public GameObject gameOver;
    public GameObject continu;
    public GameObject tim;
    public GameObject tim2;
    public Text timp;
    public Text scor , timer;
    public Image star1, star2, star3;
    public GameObject paus;
    public float delay = 3;
    public Button retry, backToMenu, backToMenu2, contin;
    public Text coins;
    public int totalCoins;
    public GameObject[] charactersArray = new GameObject[29];
    public GameObject play;
    public GameObject fadeOut;
    private string nam;
    public Animator animCont, animGM;
    public int levelIndex, j, e, m, selection , price = 100;
    public float decrease;

    public int mapSize, addedAmount, score, scoreOfChange, cooldown, bonus, highscore = -1 , score2 , score3;

    public bool ok , isEligble , spark , gm , pause , mapShrink , bonusTime, increase, shake, entered = false , done , again, once, alive, hedeadtho, tractor = true, tractor2 = true, pisuar;

    private bool needsChange;
    private float timeToWaitFade = 1f, wait;
    private int[,] b = new int [10 , 10];
    private int i = 0;

    void Retry()
    {
        pause = false;
        Time.timeScale = 1f;
        fadeOut.SetActive(true);
        if (tractor)
        {
            timeToWaitFade += Time.time;
            tractor = false;
        }
        if(Time.time >= timeToWaitFade)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ToMenu()
    {
        pause = false;
        Time.timeScale = 1f;
        fadeOut.SetActive(true);
        if(tractor2)
        {
            timeToWaitFade += Time.time;
            tractor2 = false;
        }
        if(Time.time >= timeToWaitFade)
            SceneManager.LoadScene("Main Menu");
    }

    void Continue()
    {
        int c;
        CoinData dat = CoinSystem.LoadCoins();
        c = dat.coins;
        c -= price;
        CoinSystem.SaveCoins(c);
        pisuar = true;
        wait = Time.time + 2f;
        gm = false;
        again = false;
        alive = true;
        Destroy(GameObject.Find("ContinueTimer(Clone)")); 
        animCont.SetBool("Exit", true);
        contin.enabled = false;
        if (continu.transform.localScale.x == 0)
        {
            continu.SetActive(false);
        }
        Object.Instantiate(player, new Vector3(0f, 3.85f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        nam = charactersArray[selection].ToString();
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
        play = GameObject.Find(nam);
        PlayerMovement plm = play.GetComponent<PlayerMovement>();
        spawn.player = play;
        spawn.pm = plm;
        plm.enabled = true;
    }

    void OnEnable()
    {
        retry.onClick.RemoveAllListeners();
        retry.onClick.AddListener(Retry);
        backToMenu.onClick.RemoveAllListeners();
        backToMenu.onClick.AddListener(ToMenu);
        backToMenu2.onClick.RemoveAllListeners();
        backToMenu2.onClick.AddListener(ToMenu);
        contin.onClick.RemoveAllListeners();
        contin.onClick.AddListener(Continue);
    }

    // "Track" and "Res" are called in the Fire script in order to create a fixed number of fire tiles and to keep track of them in a two dimensional array
    public bool Track (float x, float z)
    {
        int tiles = (int)((mapSize * 2 + 1) * (mapSize * 2 + 1) - mapSize * 2 - 1);
        if(i<=tiles)
        {
            i++;
            b[(int)( 5 + x / 1.2 ) , (int)( 5 + z / 1.2 )] = 1;
            return true;
        }
        return false;
    }

    // "Res" resets the two dimensional array in order to prepare it for the next wave of fire
    public void Res()
    {
        i = 0;
        int j, k;
        for (j = 1; j <= 9; j++)
            for (k = 1; k <= 9; k++)
                b[j , k] = 0;
    }

    void Awake()
    {
        pisuar = false;
        CharacterData data = CharacterSystem.CharacterLoad();
        if (data == null)
            selection = 0;
        else
            selection = data.selectedCharacterIndex;
        player = charactersArray[selection];
        alive = true;
        // From line 50 to line 71 we generate the map in order to save memory
        Quaternion rotatie;
        Vector3 pozitie;
        rotatie.x = 0f;
        rotatie.y = 0f;
        rotatie.z = 0f;
        rotatie.w = 0f;
        pozitie.y = -3f;
        pozitie.z = (mapSize + 1) * 1.2f;
        for (int i = 1; i <= 2 * mapSize + 1; i++)
        {
            pozitie.z -= 1.2f;
            pozitie.x = (mapSize + 1) * (-1.2f);
            for (int j = 1; j <= 2 * mapSize + 1; j++)
            {
                pozitie.x += 1.2f;
                pozitie.x -= 0.5f;
                pozitie.z += 0.5f;
                Object.Instantiate(pillar, pozitie, rotatie, parent.transform);
                pozitie.x += 0.5f;
                pozitie.z -= 0.5f;
            }
        }
        pozitie.x = 0f;
        pozitie.y = 3.85f;
        pozitie.z = 0f;
        Object.Instantiate(player, pozitie, rotatie);
        spawn = this.gameObject.GetComponent<Spawner>();
    }
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        ScoreData initial = ScoreSystem.ScoreLoad();
        if (initial == null)
        {
            j = 0;
            e = 0;
            m = 0;
        }
        else
        {
            j = initial.jungleScore;
            e = initial.egyptianScore;
            m = initial.medievalScore;
            if (levelIndex == 1)
                highscore = j;
            if (levelIndex == 2)
                highscore = e;
            if (levelIndex == 3)
                highscore = m;
        }
        addedAmount = 100;
        score = 0;
        ok = false;
        needsChange = false;
        isEligble = false;
        spark = false;
        mapShrink = false;
        bonusTime = false;
        increase = false;
        cooldown = 10;
        bonus = 5;
        shake = false;
        done = false;
    }

    void Update()
    {
        if (Time.time > wait)
        {
            if (pisuar == true)
            {
                price *= 2;
                pisuar = false;
            }
        }
        if (tractor == false)
            Retry();
        if (tractor2 == false)
            ToMenu();
        if(pause == true)
        {
            paus.SetActive(true);
            Time.timeScale = 0.00001f;
        }
        else
        {
            Time.timeScale = 1f;
            paus.SetActive(false);
            
        }
        if(increase == true)
        {
            delay += 0.2f;
            increase = false;
        }
        if (ok == true)
        {
            Res();
        }
        if(again == true)
        {
            hedeadtho = true;
            alive = false;
            continu.SetActive(true);
            contin.enabled = true;
            animCont.SetBool("Exit", false);
            if (once == false)
            {
                Instantiate(tim, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                tim2 = GameObject.Find("ContinueTimer(Clone)");
                ct = tim2.GetComponent<ContinueTimer>();
                once = true;
            }
            timp.text = ct.totalTime.ToString();
        }
        if (gm == true)
        {
            animCont.SetBool("Exit",true);
            if(continu.transform.localScale.x == 0)
                continu.SetActive(false);
                if (done == false)
                {
                    CoinData cdata = CoinSystem.LoadCoins();
                    if (cdata == null)
                        totalCoins = (int)float.Parse(coins.text);
                    else
                        totalCoins = cdata.coins + (int)float.Parse(coins.text);
                    CoinSystem.SaveCoins(totalCoins);
                    done = true;
                }
                score2 = score + (int)float.Parse(coins.text) * 10;
            if (score + (int)float.Parse(coins.text) * 10 > highscore)
            {
                CoinData aux;
                if (score + (int)float.Parse(coins.text) * 10 >= 1000 && highscore < 1000)
                {
                    aux = CoinSystem.LoadCoins();
                    CoinSystem.SaveCoins(aux.coins + 100);
                }
                if(score + (int)float.Parse(coins.text) * 10 >= 2000 && highscore < 2000)
                {
                    aux = CoinSystem.LoadCoins();
                    CoinSystem.SaveCoins(aux.coins + 200);
                }
                score = score + (int)float.Parse(coins.text) * 10;
                ScoreSystem.ScoreSave(this);
                score = 0;
            }
            ScoreData data = ScoreSystem.ScoreLoad();
            if (levelIndex == 1)
                highscore = data.jungleScore;
            if (levelIndex == 2)
                highscore = data.egyptianScore;
            if (levelIndex == 3)
                highscore = data.medievalScore;
            if (highscore >= 1000)
                star1.color = new Color(1, 1, 1, star1.color.a);
            if (highscore >= 2000)
                star2.color = new Color(1, 1, 1, star2.color.a);
            if (highscore >= 3000)
                star3.color = new Color(1, 1, 1, star3.color.a);
            timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, 0f);
            gameOver.SetActive(true);
        }
        if (cooldown > 0)
        {
            cooldown--;
            ok = false;
        }
        if (ok == true && cooldown == 0)
        {
            if(addedAmount > 0)
                hedeadtho = false;
            delay -= decrease;
            score += addedAmount;
            ok = false;
            needsChange = true;
            cooldown = 10;
        }
        if(mapSize < 4 && score % scoreOfChange == 0 && needsChange == true && hedeadtho == false && addedAmount != 0)
        {
            isEligble = true;
            needsChange = false;
        }
        if(mapShrink == true)
        {
            mapSize--;
            mapShrink = false;
        }
        if(isEligble == true && spark == true)
        {
            isEligble = false;
            spark = false;
            mapSize++;
            Vector3 pozitie;
            Quaternion rotatie = new Quaternion(0f, 0f, 0f, 0f);
            pozitie.y = -6f;
            pozitie.z = mapSize * 1.2f;
            pozitie.x = (mapSize + 1) * (-1.2f);
            for (int i = 1; i <= 2 * mapSize + 1; i++)
            {
                pozitie.x += 1.2f;
                pozitie.x -= 0.5f;
                pozitie.z += 0.5f;
                Object.Instantiate(pillar, pozitie, rotatie, parent.transform);
                pozitie.z -= 1f;
                Object.Instantiate(pillar, new Vector3(pozitie.x, pozitie.y, -pozitie.z), rotatie, parent.transform);
                pozitie.x += 0.5f;
                pozitie.z += 0.5f;
            }
            pozitie.z = mapSize * 1.2f;
            pozitie.x = mapSize * (-1.2f);
            for (int i = 2; i < 2 * mapSize + 1; i++)
            {
                pozitie.z -= 1.2f;
                pozitie.x -= 0.5f;
                pozitie.z += 0.5f;
                Object.Instantiate(pillar, pozitie, rotatie, parent.transform);
                pozitie.x += 1f;
                Object.Instantiate(pillar, new Vector3(-pozitie.x, pozitie.y, pozitie.z), rotatie, parent.transform);
                pozitie.x -= 0.5f;
                pozitie.z -= 0.5f;
            }
        }
    }

}
