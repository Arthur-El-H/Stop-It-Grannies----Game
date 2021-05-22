using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : IState
{
    grandma owner;

    public Chasing (grandma owner) { this.owner = owner; }

    public void Enter()
    {
    }

    public void Execute()
    {
        if (owner.getNearestDistance() < owner.getDetectionRange())
        {
            owner.chase();
        }

        else { owner.stateMachine.ChangeState(new Choosing (owner)); }
    }

    public void Exit()
    {
    }
}
