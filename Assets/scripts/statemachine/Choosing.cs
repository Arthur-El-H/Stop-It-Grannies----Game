using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choosing : IState
{
    grandma owner;

    public Choosing (grandma owner) { this.owner = owner; }

    public void Enter()
    {
    }

    public void Execute()
    {
        Vector2 cP = (Vector2) owner.transform.position;
        float randomX;
        float randomY;

        if (cP.y > 3 && cP.x < 2 && cP.x > -2)
        {
            randomY = Random.Range(-.2f, .2f);
            randomX = Random.Range(-4.0f, 4.0f);
        }

        else
        {
            randomX = Random.Range(-1.0f, 1.0f);
            randomY = Random.Range(-1.0f, 1.0f);
        }

        Vector2 dir = new Vector2(randomX, randomY);
        dir.Normalize();
        owner.stateMachine.ChangeState(new Walking(owner, dir));
    }

    public void Exit()
    {
    }
}

