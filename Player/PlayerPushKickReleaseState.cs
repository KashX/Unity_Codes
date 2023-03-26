using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushKickReleaseState : PlayerBaseState
{
    private float angleFacing;
    private float forcePower;

    public PlayerPushKickReleaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPushKickReleaseHash, true);

        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;

        if(Ctx.Timer > 1f)
        {
            forcePower = 2f;
        }
        else
        {
            forcePower = 1f;
        }

        angleFacing = Ctx.PushScript.StandPos.rotation.eulerAngles.y;          // find which side of object player is facing

        if(angleFacing == 0f)
        {
            Ctx.RB.AddForce(Vector3.right * forcePower, ForceMode.Impulse);
        }
        else if(angleFacing == 90f)
        {
            Ctx.RB.AddForce(Vector3.back * forcePower, ForceMode.Impulse);
        }
        else if(angleFacing == 180f)
        {
            Ctx.RB.AddForce(Vector3.left * forcePower, ForceMode.Impulse);
        }
        else if(angleFacing == 270f)
        {
           Ctx.RB.AddForce(Vector3.forward * forcePower, ForceMode.Impulse);
        }
    }

    public override void UpdateState()
    {
        Ctx.Timer -= Time.deltaTime;

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.Timer = 0;
        Ctx.ReadyToPush = false;
        Ctx.Animator.SetBool(Ctx.IsPushKickReleaseHash, false);
    }

    public override void CheckSwitchStates()
    {
        if(Ctx.Timer < 0)
        {
            SwitchStates(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
