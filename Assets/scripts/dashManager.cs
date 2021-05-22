using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashManager : MonoBehaviour
{
    float dashTimer;
    float dashCoolDown = 5f;
    float dashSpeed = 3f;

    bool readyToDash;
    Color rdy = new Color(255, 255, 255, .8f);
    Color unRdy = new Color(255, 255, 255, .4f);
    public GameObject rdyBtn;
    private gameManager gameManager;

    public bool p1;


    public void dash(direction dir)
    {
        if (!readyToDash) return;
        dashTimer = Time.time;
        switch (dir)
        {
            case direction.left:  StartCoroutine ("Dashing", direction.left);  break;
            case direction.right: StartCoroutine ("Dashing", direction.right); break;
            case direction.up:    StartCoroutine ("Dashing", direction.up);    break;
            case direction.down:  StartCoroutine ("Dashing", direction.down);  break;
            case direction.ul:    StartCoroutine ("Dashing", direction.ul);    break;
            case direction.ur:    StartCoroutine ("Dashing", direction.ur);    break;
            case direction.dl:    StartCoroutine ("Dashing", direction.dl);    break;
            case direction.dr:    StartCoroutine ("Dashing", direction.dr);    break;
        }
        rdyBtn.GetComponent<SpriteRenderer>().color = unRdy;
        readyToDash = false;
    }

    IEnumerator Dashing(direction dir)
    {
        gameObject.layer = 12; //invincible
        for (int i = 0; i < 30; i++)
        {
            switch (dir)
            {
                case direction.left: transform.position  = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.left,  dashSpeed * Time.deltaTime); break;
                case direction.right:transform.position  = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.right, dashSpeed * Time.deltaTime); break;
                case direction.up:   transform.position  = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.up,    dashSpeed * Time.deltaTime); break;
                case direction.down: transform.position  = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.down,  dashSpeed * Time.deltaTime); break;
                case direction.ul: transform.position    = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + new Vector2(-1, 1),  dashSpeed * Time.deltaTime); break;
                case direction.ur: transform.position    = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + new Vector2( 1, 1),  dashSpeed * Time.deltaTime); break;
                case direction.dl: transform.position    = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + new Vector2(-1, -1), dashSpeed * Time.deltaTime); break;
                case direction.dr: transform.position    = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + new Vector2( 1, -1), dashSpeed * Time.deltaTime); break;
            }
            yield return new WaitForEndOfFrame();
        }
        if (p1) gameObject.layer = 9; //stop invincible
        else gameObject.layer = 10; //stop invincible

    }

    public void Start()
    {
        gameManager = GetComponent<gameManager>();
    }
    private void Update()
    {
        if (dashTimer + dashCoolDown > Time.time) { return; }
        else { readyToDash = true; rdyBtn.GetComponent<SpriteRenderer>().color = rdy; }
    }

}
