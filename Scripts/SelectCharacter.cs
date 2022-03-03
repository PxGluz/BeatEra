using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject manager, st,dr,sel;
    private MenuManager m;
    public Button l, r, s;
    public Image left, right, select, character, blur, lok, frame;
    public Text se, index;
    public GameObject[] charactersArray = new GameObject[29];
    public Sprite[] spritesArray = new Sprite[29];

    CharacterData d;

    public int selection, selected;
    private string nam;

    void OnEnable()
    {
        l.onClick.RemoveAllListeners();
        r.onClick.RemoveAllListeners();
        s.onClick.RemoveAllListeners();
        l.onClick.AddListener(Left);
        r.onClick.AddListener(Right);
        s.onClick.AddListener(Select);
    }

    void Left()
    {
        if (selection == 1)
            selection = 29;
        else
            selection--;
        d = CharacterSystem.CharacterLoad();
    }

    void Right()
    {
        if (selection == 29)
            selection = 1;
        else
            selection++;
        d = CharacterSystem.CharacterLoad();
    }

    void Select()
    {
        selected = selection;
        int[] v = new int[29];
        CharacterData data = CharacterSystem.CharacterLoad();
        if (data == null)
            for (int i = 0; i < 29; i++)
                v[i] = 0;
        else
            for (int i = 0; i < 29; i++)
                v[i] = data.characterIndex[i];
        CharacterSystem.CharacterSave(v, selection - 1);
    }

    void Awake()
    {
        m = manager.GetComponent<MenuManager>();
        CharacterData da = CharacterSystem.CharacterLoad();
        if (da == null)
            selected = 1;
        else
            selected = da.selectedCharacterIndex + 1;
        selection = selected;
        d = CharacterSystem.CharacterLoad();
    }

    private string Name(int n)
    {
        nam = charactersArray[n - 1].ToString();
        char[] name2 = new char[50];
        int i = 0;
        while (nam[i] != '(')
            i++;
        for (int j = 0; j < i - 1; j++)
            name2[j] = nam[j];
        /*name2[i - 1] = '(';
        name2[i] = 'C';
        name2[i + 1] = 'l';
        name2[i + 2] = 'o';
        name2[i + 3] = 'n';
        name2[i + 4] = 'e';
        name2[i + 5] = ')';*/
        nam = new string(name2);
        return nam;
    }

    void Start()
    {
        
    }

    void Update()
    {
        index.text = Name(selection);
        character.sprite = spritesArray[selection - 1];
        if (selection == selected)
        {
            s.image.color = new Color(255f, 255f, 255f, s.image.color.a);
            s.interactable = true;
            index.color = new Color(0f, 255f, 0f, index.color.a);
            blur.color = new Color(blur.color.r, blur.color.g, blur.color.b, 0f);
            lok.color = new Color(lok.color.r, lok.color.g, lok.color.b, 0f);
        }
        else
        {
            if (d.characterIndex[selection-1] == 1)
            {
                s.image.color = new Color(255f, 255f, 255f, s.image.color.a);
                index.color = new Color(0.2f, 0.2f, 0.2f, index.color.a);
                s.interactable = true;
                blur.color = new Color(blur.color.r, blur.color.g, blur.color.b, 0f);
                lok.color = new Color(lok.color.r, lok.color.g, lok.color.b, 0f);
            }
            else
            {
                s.image.color = new Color(0.5f, 0.2f, 0.2f, s.image.color.a);
                index.color = new Color(1f, 0f, 0f, index.color.a);
                s.interactable = false;
                blur.color = new Color(blur.color.r, blur.color.g, blur.color.b, 0.6f);
                lok.color = new Color(lok.color.r, lok.color.g, lok.color.b, 255f);
            }
        }
        if (m.menu4 == true)
        {
            st.SetActive(true);
            dr.SetActive(true);
            sel.SetActive(true);
            if (left.color.a < 1f)
            {
                left.color = new Color(left.color.r, left.color.g, left.color.b, left.color.a + 0.05f);
                right.color = new Color(right.color.r, right.color.g, right.color.b, right.color.a + 0.05f);
                select.color = new Color(select.color.r, select.color.g, select.color.b, select.color.a + 0.05f);
                se.color = new Color(se.color.r, se.color.g, se.color.b, se.color.a + 0.05f);
                index.color = new Color(index.color.r, index.color.g, index.color.b, index.color.a + 0.05f);
                character.color = new Color(character.color.r, character.color.g, character.color.b, character.color.a + 0.05f);
                frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, frame.color.a + 0.05f);
            }
        }
        else
        {
            if (left.color.a > 0f)
            {
                left.color = new Color(left.color.r, left.color.g, left.color.b, left.color.a - 0.05f);
                right.color = new Color(right.color.r, right.color.g, right.color.b, right.color.a - 0.05f);
                select.color = new Color(select.color.r, select.color.g, select.color.b, select.color.a - 0.05f);
                se.color = new Color(se.color.r, se.color.g, se.color.b, se.color.a - 0.05f);
                index.color = new Color(index.color.r, index.color.g, index.color.b, index.color.a - 0.05f);
                character.color = new Color(character.color.r, character.color.g, character.color.b, character.color.a - 0.05f);
                frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, frame.color.a - 0.05f);
            }
            else
            {
                st.SetActive(false);
                dr.SetActive(false);
                sel.SetActive(false);
            }
        }
    }
}
