using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCoins : MonoBehaviour
{
    public Text coins;
    CoinData data;
    public GameObject capsule;
    private CapsuleMachine cm;
    void Start()
    {
        cm = capsule.GetComponent<CapsuleMachine>();
        data = CoinSystem.LoadCoins();
        if (data == null)
            coins.text = "0";
        else
            coins.text = data.coins.ToString();
    }

    void Update()
    {
        if (cm.modify == true)
        {
            data = CoinSystem.LoadCoins();
            if (data == null)
                coins.text = "0";
            else
                coins.text = data.coins.ToString();
            cm.modify = false;
        }
    }
}
