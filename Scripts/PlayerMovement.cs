using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //3.85
    public GameObject player;
    public Manager manager;
    private GameObject gameManager;
    public GameObject continu;
    public GameObject leftArm, rightArm, leftLeg, rightLeg, head, torso;
    public GameObject step,phrogg;
    

    public bool forward = false, backward = false, left = false, right = false, dead = false;
    public bool special;
    public float speed, moved , waitTime;
    private bool okDead = true;

    public int orientation;
    private float timeToWait;
    private bool ok = true;

    int cu, cd, cl, cr;
    public int stepsx, stepsz;
    private Vector3 euler;
    private Quaternion rotationEuler;

    void OnEnable()
    {
        timeToWait = Time.time + 1f;
        Destroy(GameObject.Find("Portal(Clone)"));
    }

    public void Die()
    {
        if(this.name == "Witch(Clone)")
        {
            if(okDead)
            {
                Instantiate(phrogg,new Vector3(this.transform.position.x,3.15f,this.transform.position.z),new Quaternion());
                okDead = false;
            }
            if (this.transform.localScale.x > 0f)
                this.gameObject.transform.localScale = new Vector3(this.transform.localScale.x - 0.1f,this.transform.localScale.y - 0.1f, this.transform.localScale.z - 0.1f);
            this.transform.Rotate(0f,50f,0f);

        }
        else if (this.name == "Skeleton(Clone)")
        {
            leftArm.GetComponent<BoxCollider>().enabled = true;
            leftArm.GetComponent<Rigidbody>().useGravity = true;
            rightArm.GetComponent<BoxCollider>().enabled = true;
            rightArm.GetComponent<Rigidbody>().useGravity = true;
            leftLeg.GetComponent<BoxCollider>().enabled = true;
            leftLeg.GetComponent<Rigidbody>().useGravity = true;
            rightLeg.GetComponent<BoxCollider>().enabled = true;
            rightLeg.GetComponent<Rigidbody>().useGravity = true;
            head.GetComponent<BoxCollider>().enabled = true;
            head.GetComponent<Rigidbody>().useGravity = true;
            torso.GetComponent<BoxCollider>().enabled = true;
            torso.GetComponent<Rigidbody>().useGravity = true;
            if(waitTime - 0.7f < Time.time)
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (this.name == "DJ(Clone)")
        {
            leftArm.GetComponent<BoxCollider>().enabled = true;
            rightArm.GetComponent<BoxCollider>().enabled = true;
            leftLeg.GetComponent<BoxCollider>().enabled = true;
            rightLeg.GetComponent<BoxCollider>().enabled = true;
            head.GetComponent<BoxCollider>().enabled = true;
            this.transform.localScale = new Vector3(this.transform.localScale.x - 0.015f, this.transform.localScale.y - 0.015f, this.transform.localScale.z - 0.015f);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.05f, this.gameObject.transform.position.z);
            transform.Rotate(10f, 10f, 10f, Space.World);
            if (this.gameObject.transform.localScale.x > 0f)
                this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x - 0.01f, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            if (this.gameObject.transform.localScale.y > 0f)
                this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y - 0.01f, this.gameObject.transform.localScale.z);
            if (this.gameObject.transform.localScale.z > 0f)
                this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z - 0.01f);
        }
        if (waitTime < Time.time)
        {
            if (manager.timer.GetComponent<Timer>().totalTime > 0)
            {
                CoinData data = CoinSystem.LoadCoins();
                int c;
                if (data == null)
                    c = 0;
                else
                    c = data.coins;
                if (c >= manager.price)
                    manager.again = true;
                else
                    manager.gm = true;
            }
            else
                manager.gm = true;
            if(GameObject.Find("Phrogg(Clone)") != null)
                Destroy(GameObject.Find("Phrogg(Clone)"));
            Destroy(this.gameObject);
        }
    }

    private float Fs(float x)
    {
        if (x <= 3f)
            return ((-1) * x) / 10f + 2f / 5f;
        return 0.1f;
    }
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<Manager>();
    }

    void Start()
    {
        orientation = 0;
        moved = 0;
        player.transform.position = new Vector3(0, 3.85f, 0);
        manager.addedAmount = 100;
    }
    
    void FixedUpdate()
    {
        if (timeToWait < Time.time && ok == true)
        {
            ok = false;
            this.gameObject.GetComponent<Animator>().enabled = false;
            dead = false;
            manager.gm = false;
            manager.again = false;
            manager.once = false;
        }
        speed = Fs(manager.delay);
        if (dead == false)
        {
            if (ok == false)
            {
                if (orientation == 0)
                    this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                if (orientation == 1)
                {
                    euler = new Vector3(0f, 90f, 0f);
                    rotationEuler.eulerAngles = euler;
                    this.transform.rotation = rotationEuler;
                }
                if (orientation == 2)
                {
                    euler = new Vector3(0f, 180f, 0f);
                    rotationEuler.eulerAngles = euler;
                    this.transform.rotation = rotationEuler;
                }
                if (orientation == 3)
                {
                    euler = new Vector3(0f, 270f, 0f);
                    rotationEuler.eulerAngles = euler;
                    this.transform.rotation = rotationEuler;
                }
                if (Input.GetButtonDown("Cancel"))
                {
                    if (manager.pause == false)
                        manager.pause = true;
                    else
                        manager.pause = false;
                }
                if (Input.GetKey("w"))
                {
                    if (!right && !backward && !left && stepsz < manager.mapSize)
                    {
                        forward = true;
                        orientation = 3;
                    }
                }
                if (Input.GetKey("s"))
                {
                    if (!forward && !right && !left && stepsz > -manager.mapSize)
                    {
                        backward = true;
                        orientation = 1;
                    }
                }
                if (Input.GetKey("a"))
                {
                    if (!forward && !backward && !right && stepsx > -manager.mapSize)
                    {
                        left = true;
                        orientation = 2;
                    }
                }
                if (Input.GetKey("d"))
                {
                    if (!forward && !backward && !left && stepsx < manager.mapSize)
                    {
                        
                        right = true;
                        orientation = 0;
                    }
                }
            }
        }
        else
            Die();    
        if(forward == true)
        {
            /*cu++;
            if (cu <= 6)
                player.transform.position += new Vector3(0, 0.1f, 0);
            if (cu > 6)
                player.transform.position -= new Vector3(0, 0.1f, 0);*/
            /*if (cu <= 2)
                player.transform.position -= new Vector3(0, 0, up / 2f);
            if (cu >= (numberOfTurns + 1) && cu < (3 * numberOfTurns / 2) + 1)
                player.transform.position += new Vector3(0, 0, up);
            player.transform.position += new Vector3(0, 0, up);
            if ((5 * numberOfTurns / 2) + 2 - cu <= 2 && (5 * numberOfTurns / 2) + 2 - cu > 0)
                player.transform.position -= new Vector3(0, 0, up / 2f);*/
            if (moved + speed < 1.2f)
            {
                if(moved == 0)
                    if (special)
                    {
                        Vector3 cur = new Vector3(this.transform.position.x, 3.1f, this.transform.position.z);
                        Instantiate(step, cur, new Quaternion(0, 0, 0, 0));
                    }
                this.transform.position += new Vector3(0, 0, speed);
                if(moved < 0.6f)
                    this.transform.position += new Vector3(0, 0.1f, 0);
                else
                    this.transform.position -= new Vector3(0, 0.1f, 0);
                moved += speed;
            }
            else
            {
                if (moved < 1.2f)
                {
                    this.transform.position += new Vector3(0, 0, 1.2f - moved);
                    this.transform.position -= new Vector3(0, this.transform.position.y - 3.85f, 0);
                    moved = 1.2f;
                }
            }
            if (moved == 1.2f)
            {
                moved = 0;
                stepsz++;
                forward = false;
                cu = 0;
            }
        }
        if (backward == true)
        {
            /*cd++;
            if (cd <= 6)
                player.transform.position += new Vector3(0,0.1f,0);
            if (cd > 6)
                player.transform.position -= new Vector3(0, 0.1f, 0);*/
            /*if (cd <= 2)
                player.transform.position -= new Vector3(0, 0, -dw / 2f);
            if (cd >= (numberOfTurns + 1) && cd < (3 * numberOfTurns / 2) + 1)
                player.transform.position += new Vector3(0, 0, -dw);
            player.transform.position += new Vector3(0, 0, -dw);
            if((5 * numberOfTurns / 2) + 2 - cd <= 2 && (5 * numberOfTurns / 2) + 2 - cd > 0)
                player.transform.position -= new Vector3(0, 0, -dw / 2f);*/
            if (moved + speed < 1.2f)
            {
                if (moved == 0)
                    if (special)
                    {
                        Vector3 cur = new Vector3(this.transform.position.x, 3.1f, this.transform.position.z);
                        Instantiate(step, cur, new Quaternion(0, 0, 0, 0));
                    }
                player.transform.position -= new Vector3(0, 0, speed);
                if (moved < 0.6f)
                    player.transform.position += new Vector3(0, 0.1f, 0);
                else
                    player.transform.position -= new Vector3(0, 0.1f, 0);
                moved += speed;
            }
            else
            {
                if (moved < 1.2f)
                {
                    player.transform.position -= new Vector3(0, 0, 1.2f - moved);
                    player.transform.position -= new Vector3(0, player.transform.position.y - 3.85f, 0);
                    moved = 1.2f;
                }
            }
            if (moved == 1.2f)
            {
                moved = 0;
                stepsz--;
                backward = false;
                cd = 0;
            }
        }
        if (left == true)
        {
            /*cl++;
            if (cl <= 6)
                player.transform.position += new Vector3(0, 0.1f, 0);
            if (cl > 6)
                player.transform.position -= new Vector3(0, 0.1f, 0);*/
            /*if (cl <= 2)
                player.transform.position -= new Vector3(-lf / 2f, 0, 0);
            if (cl >= (numberOfTurns + 1) && cl < (3 * numberOfTurns / 2) + 1)
                player.transform.position += new Vector3(-lf, 0, 0);
            player.transform.position += new Vector3(-lf, 0, 0);
            if ((5 * numberOfTurns / 2) + 2 - cl <= 2 && (5 * numberOfTurns / 2) + 2 - cl > 0)
                player.transform.position -= new Vector3(-lf / 2f, 0, 0);*/
            if (moved + speed < 1.2f)
            {
                if (moved == 0)
                    if (special)
                    {
                        Vector3 cur = new Vector3(this.transform.position.x, 3.1f, this.transform.position.z);
                        Instantiate(step, cur, new Quaternion(0, 0, 0, 0));
                    }
                player.transform.position -= new Vector3(speed, 0, 0);
                if (moved < 0.6f)
                    player.transform.position += new Vector3(0, 0.1f, 0);
                else
                    player.transform.position -= new Vector3(0, 0.1f, 0);
                moved += speed;
            }
            else
            {
                if (moved < 1.2f)
                {
                    player.transform.position -= new Vector3(1.2f - moved, 0, 0);
                    player.transform.position -= new Vector3(0, player.transform.position.y - 3.85f, 0);
                    moved = 1.2f;
                }
            }
            if (moved == 1.2f)
            {
                moved = 0;
                stepsx--;
                left = false;
                cl = 0;
            }
        }
        if (right == true)
        {
            /*cr++;
            if (cr <= 6)
                player.transform.position += new Vector3(0, 0.1f, 0);
            if (cr > 6)
                player.transform.position -= new Vector3(0, 0.1f, 0);*/
            /*if (cr <= 2)
                player.transform.position -= new Vector3(rt / 2f, 0, 0);
            if (cr >= (numberOfTurns + 1) && cr < (3 * numberOfTurns / 2) + 1)
                player.transform.position += new Vector3(rt, 0, 0);
            player.transform.position += new Vector3(rt, 0, 0);
            if ((5 * numberOfTurns / 2) + 2 - cr <= 2 && (5 * numberOfTurns / 2) + 2 - cr > 0)
                player.transform.position -= new Vector3(rt / 2f, 0, 0);*/
            if (moved + speed < 1.2f)
            {
                if (moved == 0)
                    if (special)
                    {
                        Vector3 cur = new Vector3(this.transform.position.x, 3.1f, this.transform.position.z);
                        Instantiate(step, cur, new Quaternion(0, 0, 0, 0));
                    }
                player.transform.position += new Vector3(speed, 0, 0);
                if (moved < 0.6f)
                    player.transform.position += new Vector3(0, 0.1f, 0);
                else
                    player.transform.position -= new Vector3(0, 0.1f, 0);
                moved += speed;
            }
            else
            {
                if (moved < 1.2f)
                {
                    player.transform.position += new Vector3(1.2f - moved, 0, 0);
                    player.transform.position -= new Vector3(0, player.transform.position.y - 3.85f, 0);
                    moved = 1.2f;
                }
            }
            if (moved == 1.2f)
            {
                moved = 0;
                stepsx++;
                right = false;
                cr = 0;
            }
        }
    }
}
