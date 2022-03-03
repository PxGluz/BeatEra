using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public bool howToPlay = false;
    public GameObject manager, fadeOut;
    private MenuManager m;
    public Button close, play;
    public Image reward, star1, star2, star3;
    public Text highscore;
    public Sprite[] rewards = new Sprite[3];
    public int level;
    private ScoreData data;
    private int[] v = new int[3];
    public Animator anim;

    IEnumerator Milbei()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void OnEnable()
    {
        anim.SetBool("Exit", false);
        close.onClick.RemoveAllListeners();
        close.onClick.AddListener(Close);
        play.onClick.RemoveAllListeners();
        play.onClick.AddListener(Play);
    }

    void Play()
    {
        fadeOut.SetActive(true);
        SceneManager.LoadScene(level);
    }

    void Close()
    {
        anim.SetBool("Exit", true);
    }

    void Start()
    {
        m = manager.GetComponent<MenuManager>();
        data = ScoreSystem.ScoreLoad();
        if (data == null)
            v[0] = v[1] = v[2] = 0;
        else
        {
            v[0] = data.jungleScore;
            v[1] = data.egyptianScore;
            v[2] = data.medievalScore;
        }
    }

    
    void Update()
    {
        if (data == null)
            highscore.text = "Highscore: N/A";
        else
            highscore.text = "Hishscore: " + v[level - 1].ToString();
        if (v[level - 1] >= 1000)
            star1.color = new Color(1, 1, 1, star1.color.a);
        if (v[level - 1] >= 2000)
            star2.color = new Color(1, 1, 1, star2.color.a);
        if (v[level - 1] >= 3000)
            star3.color = new Color(1, 1, 1, star3.color.a);
        reward.sprite = rewards[level - 1];
        if(this.transform.localScale.x == 0)
        {
            m.menu2 = true;
            howToPlay = false;
            star1.color = new Color(0, 0, 0, star1.color.a);
            star2.color = new Color(0, 0, 0, star2.color.a);
            star3.color = new Color(0, 0, 0, star3.color.a);
            this.gameObject.SetActive(false);
        }
    }
}
