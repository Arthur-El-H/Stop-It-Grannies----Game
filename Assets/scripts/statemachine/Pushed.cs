using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushed : IState
{
    grandma owner;
    Vector2 push;
    Rigidbody2D rb;
    direction dir;
    bool startHelp;
    public Pushed (grandma owner, Vector2 push) { this.owner = owner; this.push = push; }

    public void Enter()
    {
        startHelp = true;
        rb = owner.getRB();

        if (Math.Abs(push.x) > Math.Abs(push.y))
        {
            if (push.x > 0) { dir = direction.right; }
            else { dir = direction.left; }
        }
        else
        {
            if (push.y > 0) { dir = direction.up; }
            else { dir = direction.down; }
        }
    }

    public void Execute()
    {
        if (rb.velocity.magnitude > 0.1f || startHelp)
        {
            startHelp = false;
            owner.animatePush(dir);
        }
        else { owner.stateMachine.ChangeState(new Choosing(owner)); }
    }

    public void Exit()
    {
        //startHelp = false;
    }
}
