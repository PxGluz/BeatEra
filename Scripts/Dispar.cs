using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispar : MonoBehaviour
{
    private float timeToWait = 0.3f;
    public ParticleSystem euGen;

    // Start is called before the first frame update
    void Start()
    {
        Manager manager = GameObject.Find("GameManager").GetComponent<Manager>();
        this.transform.Rotate(-90f, 0, 0);
        timeToWait += Time.time;
        if (manager.selection == 21)
            this.GetComponent<ParticleSystem>().startColor = new Color(1f, 0f, 0f, 0.3f);
        if (manager.selection == 22)
            this.GetComponent<ParticleSystem>().startColor = new Color(0f, 0f, 0f, 0.3f);
        if (manager.selection == 23)
            this.GetComponent<ParticleSystem>().startColor = new Color(0.8f, 0.8f, 0.8f, 0.3f);
        if (manager.selection == 24)
            this.GetComponent<ParticleSystem>().startColor = new Color(0f, 0.5f, 1f, 0.3f);
        if (manager.selection == 25)
            this.GetComponent<ParticleSystem>().startColor = new Color(0.4f, 1f, 0.8f, 0.3f);
        if (manager.selection == 26)
            this.GetComponent<ParticleSystem>().startColor = new Color(0.7003646f, 0 , 0.7830189f, 0.3f);
        //27 is the default color (Skeleton)
        if (manager.selection == 28)
            this.GetComponent<ParticleSystem>().startColor = new Color(0, 1, 0.03f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToWait)
            Destroy(this.gameObject);
    }
}
