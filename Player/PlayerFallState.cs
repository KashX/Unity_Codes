using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if(Ctx.CharacterController.isGrounded)
        {
            SwitchStates(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if(!Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            SetSubState(Factory.Moving());
        }
    }

    private void HandleGravity()
    {
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -20.0f);
    }
}
