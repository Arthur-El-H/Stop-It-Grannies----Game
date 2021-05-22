using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : IState
{
    grandma owner;
    Vector2 direction;

    public Walking(grandma owner, Vector2 direction) { this.owner = owner; this.direction = direction; }

    Vector2 target;
    float directionMultiplier = 1f;

    public void Enter()
    {
        target = (Vector2)owner.transform.position + direction * directionMultiplier;
    }

    public void Execute()
    {
        if (keepwalking()) { owner.stateMachine.ChangeState(new Choosing(owner)); }
        if (owner.getNearestDistance() < owner.getDetectionRange())
        {
            owner.stateMachine.ChangeState(new Chasing(owner));
        }
    }

    public void Exit()
    {
    }

    bool keepwalking()
    {
        if ((Vector2)owner.transform.position == target) { return true; }
        else { owner.moveTowards(target); return false; }
    }
}
