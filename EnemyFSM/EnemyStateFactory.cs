using System.Collections.Generic;

public enum EnemyStates
{
    patrol,
    chase,
    attack,
    wait
}
public class EnemyStateFactory
{
    EnemyStateManager context;
    Dictionary<EnemyStates, EnemyBaseState> states = new Dictionary<EnemyStates, EnemyBaseState>();

    public EnemyStateFactory(EnemyStateManager currentContext)
    {
        context = currentContext;
        states[EnemyStates.patrol] = new PatrolState(context, this);
        states[EnemyStates.chase] = new ChaseState(context, this);
        states[EnemyStates.attack] = new AttackState(context, this);
        states[EnemyStates.wait] = new WaitingState(context, this);
    }

    public EnemyBaseState Patrol()
    {
        return states[EnemyStates.patrol];
    }

    public EnemyBaseState Chase()
    {
        return states[EnemyStates.chase];
    }

    public EnemyBaseState Attack()
    {
        return states[EnemyStates.attack];
    }

    public EnemyBaseState Wait()
    {
        return states[EnemyStates.wait];
    }
}
