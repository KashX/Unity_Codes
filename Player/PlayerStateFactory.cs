using System.Collections.Generic;

public enum PlayerStates
{
    grounded,
    idle,
    moving,
    jumping,
    falling,
    pushing
}
public class PlayerStateFactory
{
    PlayerStateMachine context;
    Dictionary<PlayerStates, PlayerBaseState> states = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        states[PlayerStates.grounded] = new PlayerGroundState(context, this);
        states[PlayerStates.idle] = new PlayerIdleState(context, this);
        states[PlayerStates.moving] = new PlayerMoveState(context, this);
        states[PlayerStates.jumping] = new PlayerJumpState(context, this);
        states[PlayerStates.falling] = new PlayerFallState(context, this);
        states[PlayerStates.pushing] = new PlayerPushState(context, this);
    }

    public PlayerBaseState Grounded()
    {
        return states[PlayerStates.grounded];
    }

    public PlayerBaseState Idle()
    {
        return states[PlayerStates.idle];
    }

    public PlayerBaseState Moving()
    {
        return states[PlayerStates.moving];
    }

    public PlayerBaseState Jump()
    {
        return states[PlayerStates.jumping];
    }
    
    public PlayerBaseState Fall()
    {
        return states[PlayerStates.falling];
    }

    public PlayerBaseState Push()
    {
        return states[PlayerStates.pushing];
    }

    
}
