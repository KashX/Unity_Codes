using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyStateManager ctx;
    protected EnemyStateFactory factory;
    public EnemyBaseState(EnemyStateManager currentContext, EnemyStateFactory enemyStateFactory)
    {
        ctx = currentContext;
        factory = enemyStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    protected void SwitchStates(EnemyBaseState newState)
    {
        ExitState();

        newState.EnterState();

        ctx.CurrentState = newState;
    }

}
