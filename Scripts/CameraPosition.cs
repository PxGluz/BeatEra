using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject gameManager;
    private Manager manager;
    private bool caz1, caz2, caz3, caz4, caz5, caz6;
    private int y, c;
    private Vector3 p;
    private float[] ox;
    private float[] oy;

    void Awake()
    {
        ox = new float[4];
        oy = new float[4];
        ox[0] = oy[1] = 0.05f;
        ox[2] = oy[3] = -0.05f;
        ox[1] = oy[0] = ox[3] = oy[2] = 0f;
        c = 0;
    }

    void Start()
    {
        this.transform.position = new Vector3(5.8f, 9.6f, -8.5f);
        manager = gameManager.GetComponent<Manager>();
        y = manager.mapSize;
    }

    private void determinecase(int map, int x)
    {
        x -= map;
        if(x == -1)
        {
            if (map == 2)
                caz1 = true;
            if (map == 3)
                caz2 = true;
            if (map == 4)
                caz3 = true;
        }
        if(x == 1)
        {
            if (map == 1)
                caz4 = true;
            if (map == 2)
                caz5 = true;
            if (map == 3)
                caz6 = true;
        }
    }

    void FixedUpdate()
    {
        if(manager.shake == true)
        {
            if(c < 4)
            {
                c++;
                this.transform.position += new Vector3(ox[c % 4], oy[c % 4], 0f);
            }
            else
            {
                if(c < 8)
                {
                    c++;
                    this.transform.position -= new Vector3(ox[c % 4], oy[c % 4], 0f);
                }
                else
                {
                    if(c == 8)
                    {
                        c = 0;
                        manager.shake = false;
                    }
                }
            }
        }
        /*if (manager.mapSize == 1)
            this.transform.position = new Vector3(5.8f, 9.6f, -8.5f);
        if (manager.mapSize == 2)
            this.transform.position = new Vector3(7.37f, 10.99f, -10.74f);
        if (manager.mapSize == 3)
            this.transform.position = new Vector3(10.05f, 13.38f, -14.57f);
        if (manager.mapSize == 4)
            this.transform.position = new Vector3(12.6f, 15.64f, -18.21f);*/
        determinecase(manager.mapSize, y);
        if(caz1 == true)
        {
            this.transform.position += new Vector3(1.57f / 21, 1.38f / 21, -2.24f / 21);
            p = this.transform.position;
            if (p.x >= 7.37f || p.y >= 10.99f || p.z <= -10.74f)
                caz1 = false;
        }
        if (caz2 == true)
        {
            this.transform.position += new Vector3(2.68f / 21, 2.39f / 21, -3.83f / 21);
            p = this.transform.position;
            if (p.x >= 10.05f || p.y >= 13.38f || p.z <= -14.57f)
                caz2 = false;
        }
        if (caz3 == true)
        {
            this.transform.position += new Vector3(2.55f / 21, 2.26f / 21, -3.64f / 21);
            p = this.transform.position;
            if (p.x >= 12.6f || p.y >= 15.64f || p.z <= -18.21f)
                caz3 = false;
        }
        if (caz4 == true)
        {
            this.transform.position -= new Vector3(1.57f / 21, 1.38f / 21, -2.24f / 21);
            p = this.transform.position;
            if (p.x <= 5.8f || p.y <= 9.6f || p.z >= -8.5f)
                caz4 = false;
        }
        if (caz5 == true)
        {
            this.transform.position -= new Vector3(2.68f / 21, 2.39f / 21, -3.83f / 21);
            p = this.transform.position;
            if (p.x <= 7.37f || p.y <= 10.99f || p.z >= -10.74f)
                caz5 = false;
        }
        if (caz6 == true)
        {
            this.transform.position -= new Vector3(2.55f / 21, 2.26f / 21, -3.64f / 21);
            p = this.transform.position;
            if (p.x <= 10.05f || p.y <= 13.38f || p.z >= -14.57f)
                caz6 = false;
        }
        y = manager.mapSize;
    }
}
