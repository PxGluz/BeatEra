using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    public bool entering;
    public Button but;
    public GameObject manager;
    private MenuManager m;
    public Text txt;
    public Image img;

    void OnEnable()
    {
        but.onClick.RemoveAllListeners();
        but.onClick.AddListener(Return);
    }

    void fadeOut()
    {
        if (img.color.a > 0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.05f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.05f);
        }
        else
            this.gameObject.SetActive(false);
    }

    void Return()
    {
        entering = false;
        m.menu1 = true;
        m.menu2 = false;
        m.menu3 = false;
        m.menu4 = false;
        m.change = true;
    }

    void Start()
    {
        m = manager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m.menu2 || m.menu3 || m.menu4) && img.color.a < 1f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + 0.05f);
        }
        else if (!m.menu2)
            fadeOut();
        else if (!m.menu3)
            fadeOut();
        else if (!m.menu4)
            fadeOut();
    }
}
