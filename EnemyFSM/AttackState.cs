using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public AttackState(EnemyStateManager currentContext, EnemyStateFactory enemyStateFactory) : base (currentContext, enemyStateFactory)
    {
        
    }

    public override void EnterState()
    {
        ctx.Animator.SetBool("isAttacking", true);
        ctx.Animator.SetBool("isPatrolling", false);
        ctx.Animator.SetBool("isWaiting", false);

        ctx.NavAgent.isStopped = true;
        ctx.NavAgent.speed = 0;

        AttackPlayer();
        
        ctx.CurrentTime = ctx.DelayTime;
    }

    public override void UpdateState()
    {
         ctx.CurrentTime -= 1 * Time.deltaTime;

        if(ctx.CurrentTime < 1.4f)
        {
            SwitchStates(factory.Wait());
        }
    }

    public override void ExitState()
    {
        
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking");
    }
}
