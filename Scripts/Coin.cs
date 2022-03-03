using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    public Material yellow, green, blue, purple;
    public GameObject longe, wide, thick, gameManager;
    private Manager manager;
    private float timeToWait;
    public ParticleSystem effect;
    public float value;
    public Text count;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count.text = (float.Parse(count.text) + value).ToString();
            Kill();
        }
    }
    private void Kill()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {

        Renderer rend1 = longe.GetComponent<Renderer>();
        Renderer rend2 = wide.GetComponent<Renderer>();
        Renderer rend3 = thick.GetComponent<Renderer>();
        count = GameObject.Find("Coins").GetComponent<Text>();
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
        timeToWait = Time.time + manager.delay / 2.5f;
        float chance = Random.Range(0f,100f);
        if(chance > 99f)
        {
            rend1.material = purple;
            rend2.material = purple;
            rend3.material = purple;
            value = 25f;
            effect.startColor = new Color(purple.color.r, purple.color.g, purple.color.b, 0.17f);
        }
        else if(chance > 83f)
        {
            rend1.material = blue;
            rend2.material = blue;
            rend3.material = blue;
            value = 10f;
            effect.startColor = new Color(blue.color.r, blue.color.g, blue.color.b, 0.17f);
        }
        else if(chance > 50f)
        {
            rend1.material = green;
            rend2.material = green;
            rend3.material = green;
            value = 5f;
            effect.startColor = new Color(green.color.r, green.color.g, green.color.b, 0.17f);
        }
        else
        {
            rend1.material = yellow;
            rend2.material = yellow;
            rend3.material = yellow;
            value = 1f;
            effect.startColor = new Color(yellow.color.r, yellow.color.g, yellow.color.b, 0.17f);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 5f, 0, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToWait)
        {
            Kill();
        }
    }
}
