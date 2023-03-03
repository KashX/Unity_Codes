using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsMovingHash, true);

    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.IsMovementPressed && !Ctx.IsHold)
        {
            SwitchStates(Factory.Idle());
        }

        if(Ctx.IsReadyToPush && Ctx.IsHold)
        {
            SwitchStates(Factory.Push());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
