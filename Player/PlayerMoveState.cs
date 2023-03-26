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
        if(!Ctx.IsMovementPressed && !Ctx.IsPushKick)
        {
            SwitchStates(Factory.Idle());
        }

        if(Ctx.ReadyToPush && Ctx.IsPushKick)
        {
            SwitchStates(Factory.PushKickCharge());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
