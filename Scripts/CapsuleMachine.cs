using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CapsuleMachine : MonoBehaviour
{
    public bool entering, ok = false, ok2 = true, duplicate, modify, miscaDeBit = false, mic = false;
    public Button but, startAnim, con;
    public GameObject manager, fadeOut, parent, focus, machine, capsules, selectch;
    private MenuManager m;
    private SelectCharacter chr;
    public Image img, img2, coin, prnt, disabled, rewardCapsule, character, border;
    public Sprite capsuleCommon, capsuleUncommon, capsuleRare, capsuleEpic;
    public Text txt, price, coins, tap, bcoins;
    private float target, timeToWait, timeToWait2;
    private Vector3 init;
    public int ch, bonusCoins;

    void OnEnable()
    {
        ok = false;
        but.onClick.RemoveAllListeners();
        but.onClick.AddListener(Draw);
        startAnim.onClick.RemoveAllListeners();
        startAnim.onClick.AddListener(Stop);
        con.onClick.RemoveAllListeners();
        con.onClick.AddListener(Con);
    }

    void Con()
    {
        miscaDeBit = false;
        border.transform.position = new Vector3(border.transform.position.x, 200f, border.transform.position.z);
        border.transform.localScale = new Vector3();
        rewardCapsule.gameObject.SetActive(true);
        ok = false;
        target = float.Parse(coins.text) + bonusCoins;
        modify = true;
        focus.SetActive(false);
        con.gameObject.SetActive(false);
    }

    void Stop()
    {
        CharacterData data = CharacterSystem.CharacterLoad();
        int[] v = new int[29];
        int select;
        if (data == null)
        {
            for (int i = 0; i < 29; i++)
                v[i] = 0;
            v[0] = 1;
            select = 0;
        }
        else
        {
            for (int i = 0; i < 29; i++)
                v[i] = data.characterIndex[i];
            select = data.selectedCharacterIndex;
        }
        ch = 0;
        tap.gameObject.SetActive(false);
        startAnim.interactable = false;
        timeToWait = Time.time + 2f;
        ok = true;
        float r = Random.Range(0,100f);
        if (r > 99f)
        {
            rewardCapsule.sprite = capsuleEpic;
            ch = Random.Range(26, 29);
            bonusCoins = 150;
        }
        else
        {
            if (r > 83f)
            {
                rewardCapsule.sprite = capsuleRare;
                ch = Random.Range(21, 26);
                bonusCoins = 75;
            }
            else
            {
                if (r > 50)
                {
                    rewardCapsule.sprite = capsuleUncommon;
                    ch = Random.Range(14, 21);
                    bonusCoins = 25;
                }
                else
                {
                    rewardCapsule.sprite = capsuleCommon;
                    ch = Random.Range(4, 14);
                    bonusCoins = 10;
                }
            }
        }
        if (v[ch] == 1)
        {
            duplicate = true;
            CoinData cdata = CoinSystem.LoadCoins();
            CoinSystem.SaveCoins(cdata.coins + bonusCoins);
        }
        v[ch] = 1;
        CharacterSystem.CharacterSave(v, select);
    }

    void Draw()
    {
        rewardCapsule.gameObject.SetActive(true);
        duplicate = false;
        rewardCapsule.transform.localScale = new Vector3(0,0,0);
        target = float.Parse(coins.text) - 200f;
        disabled.gameObject.SetActive(true);
        but.interactable = false;
        focus.gameObject.SetActive(true);
        tap.gameObject.SetActive(true);
        init = machine.transform.position;
        startAnim.interactable = true;
        CoinSystem.SaveCoins((int)target);
        border.transform.localScale = new Vector3(0, 0, 0);
        bcoins.transform.localScale = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        modify = false;
        target = float.Parse(coins.text);
        m = manager.GetComponent<MenuManager>();
        chr = selectch.GetComponent<SelectCharacter>();
        duplicate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m.menu3 == false)
        {
            if (disabled.color.a > 0f)
                disabled.color = new Color(disabled.color.r,disabled.color.g,disabled.color.b,disabled.color.a - 0.025f);
            if (img.color.a > 0f)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.05f);
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.05f);
                prnt.color = new Color(prnt.color.r, prnt.color.g, prnt.color.b, prnt.color.a - 0.05f);
                price.color = new Color(price.color.r, price.color.g, price.color.b, price.color.a - 0.05f);
                img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, img2.color.a - 0.05f);
                coin.color = new Color(coin.color.r, coin.color.g, coin.color.b, coin.color.a - 0.05f);
            }
            else
            {
                this.gameObject.SetActive(false);
                parent.gameObject.SetActive(false);
            }
        }
        if (m.menu3 == true && img.color.a < 1f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + 0.05f);
            prnt.color = new Color(prnt.color.r, prnt.color.g, prnt.color.b, prnt.color.a + 0.05f);
            price.color = new Color(price.color.r, price.color.g, price.color.b, price.color.a + 0.05f);
            img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, img2.color.a + 0.05f);
            coin.color = new Color(coin.color.r, coin.color.g, coin.color.b, coin.color.a + 0.05f);
        }
        if(disabled.color.a < 0.5f && m.menu3 ==true)
            disabled.color = new Color(disabled.color.r, disabled.color.g, disabled.color.b, disabled.color.a + 0.025f);
    }

    void FixedUpdate()
    {
        if(miscaDeBit)
        {
            if (border.transform.localScale.x < 1.8f)
                mic = true;
            if (border.transform.localScale.x > 2f)
                mic = false;
            if(mic == false)
            {
                border.transform.localScale = new Vector3(border.transform.localScale.x - 0.015f,border.transform.localScale.y - 0.015f,border.transform.localScale.z - 0.015f);
            }
            else
            {
                border.transform.localScale = new Vector3(border.transform.localScale.x + 0.015f, border.transform.localScale.y + 0.015f, border.transform.localScale.z + 0.015f);
            }
            if (border.transform.position.y < 500f)
                border.transform.position = new Vector3(border.transform.position.x, border.transform.position.y + 3f, border.transform.position.z);
        }
        if (float.Parse(coins.text) > target)
            coins.text = (float.Parse(coins.text) - 4f).ToString();
        else if (float.Parse(coins.text) >= 200f)
        {
            but.interactable = true;
            disabled.gameObject.SetActive(false);
        }
        if(ok)
        {
            if (Time.time > timeToWait)
            {
                machine.transform.position = init;
                capsules.transform.position = init;
                if (rewardCapsule.transform.localScale.x < 1.5f && border.transform.localScale.x < 1)
                {
                    rewardCapsule.transform.localScale = new Vector3(rewardCapsule.transform.localScale.x + 0.05f, rewardCapsule.transform.localScale.y + 0.05f, rewardCapsule.transform.localScale.z + 0.05f);
                    timeToWait2 = Time.time + 1.5f;
                }
                else
                {
                    if (Time.time <= timeToWait2)
                    {
                        if (duplicate == true)
                        {
                            bcoins.text = "Duplicate" + "\n" + "+" + bonusCoins.ToString();
                            bcoins.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        rewardCapsule.gameObject.SetActive(false);
                        character.sprite = chr.spritesArray[ch];
                        miscaDeBit = true;
                    }
                    else
                    {
                        con.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                if (ok2 == true)
                {
                    machine.transform.position = new Vector3(machine.transform.position.x - 8f, machine.transform.position.y, machine.transform.position.z);
                    capsules.transform.position = new Vector3(capsules.transform.position.x - 6.4f, capsules.transform.position.y, capsules.transform.position.z);
                    ok2 = false;
                }
                else
                {
                    machine.transform.position = new Vector3(machine.transform.position.x + 8f, machine.transform.position.y, machine.transform.position.z);
                    capsules.transform.position = new Vector3(capsules.transform.position.x + 6.4f, capsules.transform.position.y, capsules.transform.position.z);
                    ok2 = true;
                }
                //machine.transform.position = new Vector3(machine.transform.position.x + 10f, machine.transform.position.y, machine.transform.position.z);
                //capsules.transform.position = new Vector3(capsules.transform.position.x + 8f, capsules.transform.position.y, capsules.transform.position.z);
            }
        }
    }
}
