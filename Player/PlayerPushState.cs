using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState
{
    public PlayerPushState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Ctx.PushableObject.transform.parent = Ctx.PlayerObj.transform;
    }

    public override void UpdateState()
    {
        if(Ctx.IsHold)
        {
            Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
            Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
        }

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.PushableObject.transform.parent = null;
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.IsHold && Ctx.IsMovementPressed)
        {
            SwitchStates(Factory.Moving());
        }
        if(!Ctx.IsHold && !Ctx.IsMovementPressed)
        {
            SwitchStates(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
