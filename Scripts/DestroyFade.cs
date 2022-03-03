using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFade : MonoBehaviour
{
    public float timeToWait = 1f;
    // Start is called before the first frame update
    void Start()
    {
        timeToWait += Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > timeToWait)
            GameObject.Destroy(this.gameObject);
    }
}
