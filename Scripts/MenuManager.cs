using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public bool menu1 = true, menu2 = false, menu3 = false, menu4 = false;
    public Button ok,levelSelect, shop, characterSelect, back, buttonLevel1, buttonLevel2, buttonLevel3, buttonCapsuleMachine, creset, coinreset, sreset, cunlock;
    public GameObject level1, level2, level3, capsuleMachine, message;
    public LevelSelect ls;
    public Back bk;
    public Shop sh;
    public bool change, ajjutor;
    private float timeToWait;
    public Sprite[] sprites = new Sprite[3];
    public Image rew;

    void OnEnable()
    {
        ok.onClick.RemoveAllListeners();
        ok.onClick.AddListener(Ok);
        creset.onClick.RemoveAllListeners();
        creset.onClick.AddListener(CReset);
        sreset.onClick.RemoveAllListeners();
        sreset.onClick.AddListener(SReset);
        coinreset.onClick.RemoveAllListeners();
        coinreset.onClick.AddListener(CoinReset);
        cunlock.onClick.RemoveAllListeners();
        cunlock.onClick.AddListener(CUnlock);
    }
    void CUnlock()
    {
        int[] d = new int[29];
        for (int i = 0; i < 29; i++)
            d[i] = 1;
        CharacterSystem.CharacterSave(d, 0);
    }
    void Ok()
    {
        message.SetActive(false);
    }
    void CReset()
    {
        int[] v = new int[29];
        for(int i = 0; i < 29; i++)
            v[i] = 0;
        v[0] = 1;
        CharacterSystem.CharacterSave(v, 0);
    }
    void CoinReset()
    {
        CoinSystem.SaveCoins(5000);
    }

    void SReset()
    {
        Manager m = new Manager();
        m.score = 0;
        m.levelIndex = 1;
        ScoreSystem.ScoreSave(m);
        m.levelIndex = 2;
        ScoreSystem.ScoreSave(m);
        m.levelIndex = 3;
        ScoreSystem.ScoreSave(m);
    }
    void Awake()
    {
        CharacterData data = CharacterSystem.CharacterLoad();
        if (data == null)
        {
            int[] v = new int[29];
            for (int i = 0; i < 29; i++)
                v[i] = 0;
            v[0] = 1;
            CharacterSystem.CharacterSave(v, 0);
        }
        else
        {
            data.characterIndex[0] = 1;
            CharacterSystem.CharacterSave(data.characterIndex, data.selectedCharacterIndex);
        }
    }
    void Start()
    {
        CharacterData data = CharacterSystem.CharacterLoad();
        if (data != null)
        {
            if (ScoreSystem.ScoreLoad().jungleScore >= 3000 && CharacterSystem.CharacterLoad().characterIndex[1] == 0)
            {
                rew.sprite = sprites[0];
                data.characterIndex[1] = 1;
                message.SetActive(true);
            }
            else
            {
                if (ScoreSystem.ScoreLoad().egyptianScore >= 3000 && CharacterSystem.CharacterLoad().characterIndex[2] == 0)
                {
                    rew.sprite = sprites[1];
                    data.characterIndex[2] = 1;
                    message.SetActive(true);
                }
                else
                {
                    if (ScoreSystem.ScoreLoad().medievalScore >= 3000 && CharacterSystem.CharacterLoad().characterIndex[3] == 0)
                    {
                        rew.sprite = sprites[2];
                        data.characterIndex[3] = 1;
                        message.SetActive(true);
                    }
                }
            }
            CharacterSystem.CharacterSave(data.characterIndex, data.selectedCharacterIndex);
        }
        ls = levelSelect.gameObject.GetComponent<LevelSelect>();
        bk = back.gameObject.GetComponent<Back>();
        sh = shop.gameObject.GetComponent<Shop>();
        change = false;
        ajjutor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(change == true)
        {
            ajjutor = true;
            timeToWait = Time.time + 0.25f;
            change = false;
        }
        if (Time.time > timeToWait && ajjutor == true)
        {
            GameObject.Find("Select_Character").GetComponent<SelectCharacter>().selection = GameObject.Find("Select_Character").GetComponent<SelectCharacter>().selected;
            ajjutor = false;
        }
        if (menu1 == true)
        {
            levelSelect.gameObject.SetActive(true);
            ls.entering = true;
            shop.gameObject.SetActive(true);
            sh.entering = true;
            characterSelect.gameObject.SetActive(true);
        }
        if (menu2 == true)
        {
            level1.gameObject.SetActive(true);
            level2.gameObject.SetActive(true);
            level3.gameObject.SetActive(true);
            buttonLevel1.gameObject.SetActive(true);
            buttonLevel2.gameObject.SetActive(true);
            buttonLevel3.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            bk.entering = true;
        }
        if (menu3 == true)
        {
            buttonCapsuleMachine.gameObject.SetActive(true);
            capsuleMachine.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            bk.entering = true;
        }
        if(menu4 == true)
        {
            back.gameObject.SetActive(true);
            bk.entering = true;
        }
    }
}
