using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{

    public GameObject gameManager;
    private Manager manager;
    private float timeToWait;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("vibrez incet");
        if(collision.gameObject.tag == "Player")
        {
            manager.bonusTime = true;
            //Debug.Log("vibrez");
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        timeToWait = Time.time + manager.delay / 2.5f;
    }


    void FixedUpdate()
    {
        transform.Rotate(0, 5f, 0, Space.World);
    }

    void Update()
    {
        if (Time.time >= timeToWait)
        {
            Kill();
        }
    }
}
