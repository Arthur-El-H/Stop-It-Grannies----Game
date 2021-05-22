using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    Move moveManager;
    dashManager dashManager;
    pushManager pushManager;
    //dash timers
    float lTimer;
    float rTimer;
    float uTimer;
    float dTimer;
    float doubleClickTime = .2f;

    bool waiting;
    int framesToWait;

    public bool player1;

    void Start()
    {
        moveManager = GetComponent<Move>();
        dashManager = GetComponent<dashManager>();
        pushManager = GetComponent<pushManager>();

        dashManager.p1 = player1;
    }
    // Update is called once per frame
    void Update()
    {
        if (player1)
        {
            if (waiting) { framesToWait--; if (framesToWait == 0) { waiting = false; } return; }

            if (Input.GetKey(KeyCode.A)) { moveManager.moveLeft(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.D)) { moveManager.moveRight(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.W)) { moveManager.moveUp(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.S)) { moveManager.moveDown(); pushManager.zeroPushBar(); }

            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                if (Input.GetKeyUp("space")) pushManager.push();
                if (Input.GetKey  ("space")) pushManager.loadPush();
            }

            if (Input.GetKeyDown(KeyCode.A)) if (lTimer + doubleClickTime > Time.time) dash(direction.left); else lTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.D)) if (rTimer + doubleClickTime > Time.time) dash(direction.right);else rTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.W)) if (uTimer + doubleClickTime > Time.time) dash(direction.up);   else uTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.S)) if (dTimer + doubleClickTime > Time.time) dash(direction.down); else dTimer = Time.time;

            if (!Input.anyKey) { moveManager.stand(); }

            if (Input.GetKeyUp(KeyCode.A)) { moveManager.stopLeft(); }
            if (Input.GetKeyUp(KeyCode.D)) { moveManager.stopRight(); }
            if (Input.GetKeyUp(KeyCode.W)) { moveManager.stopUp(); }
            if (Input.GetKeyUp(KeyCode.S)) { moveManager.stopDown(); }

        }
        
        else
        {
            if (waiting) { framesToWait--; if (framesToWait == 0) { waiting = false; } return; }

            if (Input.GetKey(KeyCode.LeftArrow)) { moveManager.moveLeft(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.RightArrow)) { moveManager.moveRight(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.UpArrow)) { moveManager.moveUp(); pushManager.zeroPushBar(); }
            if (Input.GetKey(KeyCode.DownArrow)) { moveManager.moveDown(); pushManager.zeroPushBar(); }

            if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                if (Input.GetKeyUp(KeyCode.Return)) pushManager.push();
                if (Input.GetKey(KeyCode.Return)) pushManager.loadPush();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) if (lTimer + doubleClickTime > Time.time) dash(direction.left); else lTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.RightArrow))if (rTimer + doubleClickTime > Time.time) dash(direction.right);else rTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.UpArrow))   if (uTimer + doubleClickTime > Time.time) dash(direction.up);   else uTimer = Time.time;
            if (Input.GetKeyDown(KeyCode.DownArrow)) if (dTimer + doubleClickTime > Time.time) dash(direction.down); else dTimer = Time.time;

            if (!Input.anyKey) { moveManager.stand(); }

            if (Input.GetKeyUp(KeyCode.LeftArrow))  { moveManager.stopLeft(); }
            if (Input.GetKeyUp(KeyCode.RightArrow)) { moveManager.stopRight(); }
            if (Input.GetKeyUp(KeyCode.UpArrow))    { moveManager.stopUp(); }
            if (Input.GetKeyUp(KeyCode.DownArrow))  { moveManager.stopDown(); }
        }
    }

    private void dash (direction dir)
    {
        if (!player1)
        {
            switch (dir)
            {
                case direction.up:
                    if      (Input.GetKey(KeyCode.LeftArrow))  { dashManager.dash(direction.ul); }
                    else if (Input.GetKey(KeyCode.RightArrow)) { dashManager.dash(direction.ur); }
                    else                                       { dashManager.dash(direction.up); }
                    break;
                case direction.down:
                    if      (Input.GetKey(KeyCode.LeftArrow))  { dashManager.dash(direction.dl); }
                    else if (Input.GetKey(KeyCode.RightArrow)) { dashManager.dash(direction.dr); }
                    else                                       { dashManager.dash(direction.down); }
                    break;
                case direction.left:
                    if      (Input.GetKey(KeyCode.UpArrow))   { dashManager.dash(direction.ul); }
                    else if (Input.GetKey(KeyCode.DownArrow)) { dashManager.dash(direction.dl); }
                    else                                      { dashManager.dash(direction.left); }
                    break;
                case direction.right:
                    if      (Input.GetKey(KeyCode.UpArrow))   { dashManager.dash(direction.ur); }
                    else if (Input.GetKey(KeyCode.DownArrow)) { dashManager.dash(direction.dr); }
                    else                                      { dashManager.dash(direction.right); }
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case direction.up: 
                    if     (Input.GetKey(KeyCode.A))   { dashManager.dash(direction.ul); }
                    else if(Input.GetKey(KeyCode.D))   { dashManager.dash(direction.ur); }
                    else                               { dashManager.dash(direction.up); }
                    break;
                case direction.down:
                    if      (Input.GetKey(KeyCode.A))  { dashManager.dash(direction.dl); }
                    else if (Input.GetKey(KeyCode.D))  { dashManager.dash(direction.dr); }
                    else                               { dashManager.dash(direction.down); }
                    break;
                case direction.left:
                    if      (Input.GetKey(KeyCode.W))  { dashManager.dash(direction.ul); }
                    else if (Input.GetKey(KeyCode.S))  { dashManager.dash(direction.dl); }
                    else                               { dashManager.dash(direction.left); }
                    break;
                case direction.right:
                    if      (Input.GetKey(KeyCode.W))  { dashManager.dash(direction.ur); }
                    else if (Input.GetKey(KeyCode.S))  { dashManager.dash(direction.dr); }
                    else                               { dashManager.dash(direction.right); }
                    break;
            }
            
        }
            
    }

    public void waitFor(int frames)
    {
        framesToWait = frames;
        waiting = true;        
    }
}

