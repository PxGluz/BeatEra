using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EgyptianLevel : MonoBehaviour
{
    public bool entering;
    public Button but;
    public GameObject manager;
    public GameObject parent;
    public GameObject fadeOut;
    public GameObject htp;
    private MenuManager m;
    private float timeToWait = 1f;
    private bool ok = false, ok2 = false;
    public Image img, prnt, star1, star2, star3;
    public Text txt, high;

    void OnEnable()
    {
        but.onClick.RemoveAllListeners();
        but.onClick.AddListener(Enter);
    }

    void Enter()
    {
        htp.GetComponent<HowToPlay>().howToPlay = true;
        htp.GetComponent<HowToPlay>().level = 2;
        htp.SetActive(true);
        m.menu2 = false;
        /*ok = true;
        timeToWait += Time.time;
        if (ok2 == true)
        {
            SceneManager.LoadScene("Egyptian Level");
        }
        fadeOut.SetActive(true);*/
    }

    // Start is called before the first frame update
    void Start()
    {
        m = manager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m.menu2 == false)
        {
            if (img.color.a > 0f)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.05f);
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.05f);
                prnt.color = new Color(prnt.color.r, prnt.color.g, prnt.color.b, prnt.color.a - 0.05f);
                high.color = new Color(high.color.r, high.color.g, high.color.b, high.color.a - 0.05f);
                star1.color = new Color(star1.color.r, star1.color.g, star1.color.b, star1.color.a - 0.05f);
                star2.color = new Color(star2.color.r, star2.color.g, star2.color.b, star2.color.a - 0.05f);
                star3.color = new Color(star3.color.r, star3.color.g, star3.color.b, star3.color.a - 0.05f);
            }
            else
            {
                this.gameObject.SetActive(false);
                parent.gameObject.SetActive(false);
            }
        }
        if (m.menu2 == true && img.color.a < 1f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + 0.05f);
            prnt.color = new Color(prnt.color.r, prnt.color.g, prnt.color.b, prnt.color.a + 0.05f);
            high.color = new Color(high.color.r, high.color.g, high.color.b, high.color.a + 0.05f);
            star1.color = new Color(star1.color.r, star1.color.g, star1.color.b, star1.color.a + 0.05f);
            star2.color = new Color(star2.color.r, star2.color.g, star2.color.b, star2.color.a + 0.05f);
            star3.color = new Color(star3.color.r, star3.color.g, star3.color.b, star3.color.a + 0.05f);
        }
        /*if (ok == true)
        {
            if (Time.time > timeToWait)
            {
                ok2 = true;
                Enter();
            }
        }*/

    }
}
