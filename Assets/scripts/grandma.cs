using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandma : MonoBehaviour
{
    float maxRange;
    float speed = .45f;
    float detectionRange = 5f;
    public float getDetectionRange() { return detectionRange; }


    float distanceOne;
    float distanceTwo;
    public float nearestDistance;
    public float getNearestDistance() { return nearestDistance; }

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject nearest;

    private Animator animator;
    private Rigidbody2D rigidBody; public Rigidbody2D getRB() { return rigidBody; }
    private AudioSource audio;

    public gameManager gameManager;
    public grannyManager grannyManager;
    public grannyStateMachine stateMachine = new grannyStateMachine();

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine.ChangeState(new Choosing(this));
    }
 
    void Update()
    {
        actualizeDistance();
        stateMachine.Update();
    }

    void actualizeDistance()
    {
        distanceOne = Vector2.Distance((Vector2)transform.position, (Vector2)playerOne.transform.position);
        distanceTwo = Vector2.Distance((Vector2)transform.position, (Vector2)playerTwo.transform.position);

        if (distanceOne < distanceTwo) { nearest = playerOne; nearestDistance = distanceOne; }
        else { nearest = playerTwo; nearestDistance = distanceTwo; }
    }

    public void moveTowards(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        animateWalking(direction);

        transform.position = Vector2.MoveTowards((Vector2) transform.position, target, speed * Time.deltaTime);
    }

    void animateWalking(Vector2 dir) 
    { 
        if (Math.Abs(dir.x) > Math.Abs(dir.y)) 
        {
            if (dir.x > 0) { animator.Play("Oma_Right"); }
            else { animator.Play("Oma_Left"); }
        }
        else
        {
            if (dir.y > 0) { animator.Play("Oma_Backward"); }
            else { animator.Play("Oma_Forward"); }
        }
    }

    public void beingPushed(Vector2 push)
    {
        audio.Play();
        rigidBody.AddForce(push);
        stateMachine.ChangeState(new Pushed(this, push));
    }

    public void animatePush(direction dir)
    {
        switch (dir)
        {
            case direction.up: animator.Play("SlideUp"); break;
            case direction.down: animator.Play("SlideDown"); break;
            case direction.left: animator.Play("SlideLeft"); break;
            case direction.right: animator.Play("SlideRight"); break;
        }
    }

    public void chase()
    {
        moveTowards((Vector2)nearest.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8 )
        {
            stateMachine.ChangeState(new Choosing(this));
        }

        if (collision.gameObject.layer == 9)        //in grannymanager damit es für alle omas gilt!
        {
            grannyManager.loose(false);
        }

        if (collision.gameObject.layer == 10)
        {
            grannyManager.loose(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stateMachine.ChangeState(new Choosing(this));
        }
    }
}
