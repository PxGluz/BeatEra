using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int waveAtCreation;

    public GameObject fire;
    public GameObject shrinkmap;
    public GameObject addtime;

    private GameObject gameManager;
    public Manager manager;

    private Vector3 pos = new Vector3( 0, 3.2f, 0);
    private Quaternion rot = new Quaternion( 0, 0, 0, 0);

    private float timeToWait = 3f;
    float shakeTime, animationTime;
    float edgeRight, edgeLeft;
    public Material newMaterial;
    private bool ok = true;

    public void CreateFire()
    {
        manager.spark = true;
        int t = Random.Range(0,100);
        // Here we try to randomly choose whether or not to set this tile on fire
        if( t % ((manager.mapSize*2)+1) != 0 )
        {
            float x = this.transform.position.x;
            float z = this.transform.position.z;
            if (manager.Track(x,z))
            {
                pos.x = this.gameObject.transform.position.x + 0.5f;
                pos.z = this.gameObject.transform.position.z - 0.5f;
                Object.Instantiate(fire, new Vector3(pos.x,this.transform.position.y + 6.2f,pos.z), rot);
            }
        }
        // Shrink size power up
        /*if (waveAtCreation < manager.mapSize)
        {
            int x = Random.Range(0, 100);
            if (x == 1 || x == 2)
            {
                Vector3 p = new Vector3(0.5f, 3.5f, -0.5f);
                Quaternion r = new Quaternion(0, 45f, 0, 90f);
                Object.Instantiate(shrinkmap, p, r);
            }
        }*/
        //Bonus time power up(unoptimized spawn rate)
        /*int y = Random.Range(0, 100);
        if(y == 1)
        {
            Vector3 p = new Vector3(0f, 3.5f, 0f);
            Quaternion r = new Quaternion(0, 45f, 0, 90f);
            Object.Instantiate(addtime, p, r);
        }*/
    }

    void Awake()
    {

    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        waveAtCreation = manager.mapSize;
        edgeRight = this.transform.position.x + 0.05f;
        edgeLeft = this.transform.position.x - 0.05f;
        timeToWait = Time.time + manager.delay;
        if (manager.entered)
            CreateFire();
    }

    void FixedUpdate()
    {
        if(this.transform.position.y < -3.1f)
        {
            transform.position = new Vector3(this.transform.position.x ,this.transform.position.y + 0.1f ,this.transform.position.z);
        }
        Renderer rend = GetComponent<Renderer>();
        rend.material = newMaterial;
        if (Time.time >= timeToWait && manager.entered)
        {
            timeToWait = Time.time + manager.delay;
            CreateFire();
        }
        if(waveAtCreation > manager.mapSize && ok==true)
        {
            shakeTime = Time.time + 0.5f;
            animationTime = Time.time + 1.5f;
            ok = false;
        }
        if(waveAtCreation > manager.mapSize && ok==false)
        {

            if (Time.time <= shakeTime)
            {
                if (this.transform.position.x >= edgeRight)
                {
                    this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z - 0.01f);
                }
                else
                {
                    if (this.transform.position.x <= edgeLeft)
                    {
                        this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z + 0.01f);

                    }
                    else
                    {
                        this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z - 0.01f);
                    }
                }
            }
            else
            {
                if (Time.time > animationTime)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z);
                }
            }
        }
    }
}
