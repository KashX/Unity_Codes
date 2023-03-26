using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushKickChargeState : PlayerBaseState
{
    public PlayerPushKickChargeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPushKickChargeHash, true);

        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;

        Ctx.Timer = 0;
    }

    public override void UpdateState()
    {
        Debug.Log(Ctx.Timer);
        Ctx.Timer += Time.deltaTime;
        if(Ctx.Timer > 2f)
        {
            Ctx.Timer = 2f;
        }
        
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsPushKickChargeHash, false);
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.IsPushKick)
        {
            SwitchStates(Factory.PushKickRelease());
        }
    }

    public override void InitializeSubState()
    {
        
    }

    private void HandleIK()
    {
        
    }
}
