using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
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
        if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchStates(Factory.Jump());
        }
        else if(!Ctx.CharacterController.isGrounded)
        {
            SwitchStates(Factory.Fall());
        }
    }

    public override void InitializeSubState()
    {
        if(Ctx.IsReadyToPush)
        {
            SetSubState(Factory.Push());
        }
        else if(!Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if(Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Moving());
        }
    }
}
