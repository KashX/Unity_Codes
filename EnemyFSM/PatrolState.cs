using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(EnemyStateManager currentContext, EnemyStateFactory enemyStateFactory) : base (currentContext, enemyStateFactory)
    {
        
    }

    public override void EnterState()
    {
        ctx.Animator.SetBool("isPatrolling", true);
        ctx.Animator.SetBool("isAttacking", false);
        ctx.Animator.SetBool("isWaiting", false);

        ctx.NavAgent.stoppingDistance = 0.0f;
        ctx.NavAgent.speed = ctx.PatrolSpeed;

        UpdateDestination();
        //ctx.TargetWayPoint = ctx.WayPoints[ctx.Index].position;
    }

    public override void UpdateState()
    {
        UpdateWayPoints();
        UpdateDestination();

        if(ctx.DetectedPlayer)
        {
            Debug.Log("Switching to Chase State");
            SwitchStates(factory.Chase());
        }
    }

    public override void ExitState()
    {
        
    }

    private void UpdateDestination()
    {
        ctx.TargetWayPoint = ctx.WayPoints[ctx.Index].position;
        ctx.NavAgent.SetDestination(ctx.TargetWayPoint);
    }

    private void UpdateWayPoints()
    {
        // Updating target Waypoints
        if(Vector3.Distance(ctx.transform.position, ctx.TargetWayPoint) < 0.5f)
        {
            ctx.Index = (ctx.Index + 1) % ctx.WayPoints.Length;
        }
    }
}
