using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsMovingHash, false);

        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if(Ctx.IsMovementPressed && !Ctx.IsPushKick)
        {
            SwitchStates(Factory.Moving());
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
