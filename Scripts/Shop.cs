using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public bool entering = true;
    public GameObject manager;
    private MenuManager m;
    public Button but;
    public Image img;
    public Text txt;


    void OnEnable()
    {
        but.onClick.RemoveAllListeners();
        but.onClick.AddListener(Switch);
    }

    void Switch()
    {
        entering = false;
        m.menu3 = true;
        m.menu1 = false;
    }

    void Start()
    {
        m = manager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m.menu1 == false)
        {
            if (img.color.a > 0f)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.05f);
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.05f);
            }
            else
                this.gameObject.SetActive(false);
        }
        if (m.menu1 == true && img.color.a < 1f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + 0.05f);
        }
    }
}
