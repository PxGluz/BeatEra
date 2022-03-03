using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKiller : MonoBehaviour
{

    private GameObject gameManager;
    private GameObject Fire;
    public Manager manager;
    public Fire fire;
    public float timeToWait;
    private GameObject player;
    private PlayerMovement pl;
    public GameObject portal;
    private bool ok = false;

    public Material newMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            pl = player.GetComponent<PlayerMovement>();
            manager.addedAmount = 0;
            Vector3 pos = new Vector3(player.transform.position.x, 6.5f, player.transform.position.z);
            if (ok == false && player.name != "Skeleton(Clone)" && player.name != "DJ(Clone)" && player.name != "Witch(Clone)")
            {
                Object.Instantiate(portal, pos, new Quaternion(0, 0, 0, 0));
                portal = GameObject.Find("Portal(Clone)");
                portal.transform.Rotate(-90f, 0, 0);
            }
            ok = true;
            pl.waitTime = Time.time + 1f;
            pl.dead = true;
            pl.Die();
        }
    }
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        timeToWait = Time.time + manager.delay / 2.5f;
    }
    void Kill()
    {
        manager.ok = true;
        manager.spark = false;
        Destroy(this.gameObject);
    }

    void Start()
    {

    }

    void FixedUpdate()
    {  
        if (Time.time >= timeToWait)
        {
            Kill();
        }
    }
}
