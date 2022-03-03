using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private GameObject Fire;
    private GameObject gameManager;
    public Manager manager;
    public Fire fire;
    public float timeToWait;
    private int y;
    public int waveAtCreation;

    public GameObject killAura;

    void Kill()
    {
        Object.Instantiate(killAura, new Vector3(this.gameObject.transform.position.x, 3.05f, this.gameObject.transform.position.z), this.gameObject.transform.rotation);
        manager.shake = true;
        Destroy(this.gameObject);
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        timeToWait = Time.time + manager.delay / 2.5f;
        y = manager.mapSize;
    }
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (this.transform.position.y < 3.2)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f,this.transform.position.z);
        if (Time.time >= timeToWait)
        {
            Kill();
        }
        if (y - manager.mapSize == 1)
        {
            Destroy(this.gameObject);
        }
        y = manager.mapSize;
    }
}
