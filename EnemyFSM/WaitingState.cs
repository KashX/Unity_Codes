using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : EnemyBaseState
{
    public WaitingState(EnemyStateManager currentContext, EnemyStateFactory enemyStateFactory) : base (currentContext, enemyStateFactory)
    {
        
    }

    public override void EnterState()
    {
        ctx.Animator.SetBool("isWaiting", true);
        ctx.Animator.SetBool("isPatrolling", false);
        ctx.Animator.SetBool("isAttacking", false);

        ctx.NavAgent.speed = 0;

        ctx.CurrentTime = ctx.DelayTime;

        Debug.Log("Entered Waiting STate");
    }

    public override void UpdateState()
    {
        ctx.CurrentTime -= 1 * Time.deltaTime;

        if(ctx.CurrentTime <= 0)
        {
            if(ctx.DetectedPlayer)
                SwitchStates(factory.Chase());
            else if(!ctx.DetectedPlayer)
                SwitchStates(factory.Patrol());
        }
        
    }

    public override void ExitState()
    {
        ctx.NavAgent.isStopped = false;
    }
}
