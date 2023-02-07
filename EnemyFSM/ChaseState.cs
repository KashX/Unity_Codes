using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    public ChaseState(EnemyStateManager currentContext, EnemyStateFactory enemyStateFactory) : base (currentContext, enemyStateFactory)
    {
        
    }

    public override void EnterState()
    {
        ctx.Animator.SetBool("isPatrolling", true);
        ctx.Animator.SetBool("isAttacking", false);
        ctx.Animator.SetBool("isWaiting", false);

        ctx.NavAgent.stoppingDistance = 0.5f;
        ctx.NavAgent.speed = ctx.ChaseSpeed;
        Debug.Log("Entered Chase State");
    }

    public override void UpdateState()
    {
        ctx.NavAgent.SetDestination(ctx.Player.position);

        if(Vector3.Distance(ctx.transform.position, ctx.Player.position) <= 0.7f)
        {
            Debug.Log("Switching to Attack State");
            SwitchStates(factory.Attack());
        }
    }

    public override void ExitState()
    {
        
    }
}
