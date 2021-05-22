using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushManager : MonoBehaviour
{
    List<grandma> grandmas = new List<grandma>();
    //public grandma testma;
    float pushDistance = 2.3f;

    InputManager inputManager;
    public waveMaker waveMaker;
    public grannyManager grannyManager;

    public GameObject burnt;


    float pushBar;
    float pushBarMax = 180;

    float pushConstant = 120f;

    private Animator animator;
    public AudioSource charge;
    public AudioSource pushSound;


    int blastersHolded;
    public GameObject pushBtn1;
    public GameObject pushBtn2;
    public GameObject pushBtn3;
    List<GameObject> blasters;
    GameObject currentBlaster() { return blasters[blastersHolded - 1]; }

    public Color off;
    public Color available;
    public Color max;

    bool p1;

    private void changePushBtnColor(GameObject Btn,  Color color)
    {
        Btn.GetComponent<SpriteRenderer>().color = color;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        blasters = new List<GameObject> { pushBtn1, pushBtn2, pushBtn3 };
        changePushBtnColor(pushBtn1, off);
        changePushBtnColor(pushBtn2, off);
        changePushBtnColor(pushBtn3, off);

    }


    bool maxLoaded;

    public void loadPush()
    {
        if (blastersHolded == 0) { return; }

        if (!charge.isPlaying) {charge.Play();}
        animator.Play("loadPush");

        if (pushBar < pushBarMax)
        {
            pushBar++;
        }

        else
        {
            if (!maxLoaded)
            {
                changePushBtnColor(currentBlaster(), max );
                maxLoaded = true;
            }
        }
    }

    public void push()
    {
        if (blastersHolded == 0) { return; }
        inputManager.waitFor(60);
        StartCoroutine(Pushing());
        Instantiate(burnt, transform.position + new Vector3(-.561f, -.3f, 0), Quaternion.identity); 
        foreach (grandma grandma in grannyManager.grannys)
        {
            if (Vector2.Distance((Vector2)grandma.transform.position, (Vector2)transform.position) < pushDistance)
            {
                Vector2 pushDirection = (Vector2)grandma.transform.position - (Vector2)transform.position;
                pushDirection.Normalize();
                float chargMult = (pushBar / pushBarMax);
                Vector2 finalPush = pushDirection * pushConstant * (1 + chargMult);
                grandma.beingPushed(finalPush);
                grandma.GetComponent<Rigidbody2D>().AddForce(pushDirection);
            }
        }
        pushSound.Play();
        charge.Stop();
        pushBar = 0;
        maxLoaded = false;
        changePushBtnColor(currentBlaster(), off);
        blastersHolded--;
    }

    IEnumerator Pushing()
    {
        waveMaker.createWave((Vector2)transform.position);
        for (int i = 0; i < 60; i++)
        {
            animator.Play("Push");
            yield return null;
        }
    }

    public void zeroPushBar()
    {
        if(blastersHolded == 0) { return; }
        charge.Stop();
        pushBar = 0;
        maxLoaded = false;
        changePushBtnColor(currentBlaster(), available);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 && (blastersHolded < 3))
        {
            Destroy(collision.gameObject);
            if (maxLoaded)
            {
                changePushBtnColor(currentBlaster(), available);
                blastersHolded++;
                changePushBtnColor(currentBlaster(), max);
            }
            else
            {
                blastersHolded++;
                changePushBtnColor(currentBlaster(), available);
            }
        }
    }
}
