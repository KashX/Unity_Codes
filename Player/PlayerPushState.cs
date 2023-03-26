using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState
{
    private float angleFacing;

    private float pushMoveEast;
    private float pushMoveWest;
    private float pushMoveNorth;
    private float pushMoveSouth;

    public PlayerPushState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base (currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("In Pushing State");

        Ctx.RB.isKinematic = false;

        angleFacing = Ctx.PushScript.StandPos.rotation.eulerAngles.y;
    }

    public override void UpdateState()
    {
        CheckPushMovementDirection();
        Debug.Log(angleFacing);

        if(angleFacing == 0f)
        {
            Ctx.AppliedMovementX = pushMoveWest;
        }
        else if(angleFacing == 90f)
        {
            Ctx.AppliedMovementZ = pushMoveNorth;
        }
        else if(angleFacing == 180f)
        {
            Ctx.AppliedMovementX = pushMoveEast;
        }
        else if(angleFacing == 270f)
        {
            Ctx.AppliedMovementZ = pushMoveSouth;
        }

        RigidbodyCalculations();

        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Pushing state");

        Ctx.RB.isKinematic = true;
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

    private void HandleIK()
    {
        Ctx.LeftHandIK.data.target = Ctx.PushScript.LeftHandPos;
        Ctx.RightHandIK.data.target = Ctx.PushScript.RightHandPos;

        Ctx.PlayerTransform.position = Ctx.PushScript.StandPos.position;
        Ctx.PlayerTransform.rotation = Ctx.PushScript.StandPos.localRotation;
    }

    private void CheckPushMovementDirection()
    {
        if(Ctx.CurrentMovementInput.x > 0)
        {
            pushMoveWest = Ctx.CurrentMovementInput.x;
        }
        if(Ctx.CurrentMovementInput.x < 0)
        {
            pushMoveEast = Ctx.CurrentMovementInput.x;
        }
        if(Ctx.CurrentMovementInput.y > 0)
        {
            pushMoveSouth = Ctx.CurrentMovementInput.y;
        }
        if(Ctx.CurrentMovementInput.y < 0)
        {
            pushMoveNorth = Ctx.CurrentMovementInput.y;
        }
    }

    private void RigidbodyCalculations()
    {
        if(Ctx.RB != null)
        {
            // Vector3 forceDirection = (hit.gameObject.transform.position - transform.position).normalized;
            Vector3 forceDirection = Ctx.PlayerTransform.forward.normalized;
            forceDirection.y = 0;

            Ctx.RB.AddRelativeForce(forceDirection * 1f, ForceMode.Force);
        }
    }
}
