using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory) {
        InitSubState();
    }
    public override void EnterState()
    {
        Debug.Log("PState: enter Grounded");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        
    }
    public override void ExitState()
    {
        Debug.Log("PState: exit Grounded");
    }
    public override void InitSubState() {

    }
    public override void CheckSwitchStates()
    {
        if (_context.IsMoving) {
            SwitchState(_stateFactory.Walk());
        }
        if (_context.IsJumping) {
            SwitchState(_stateFactory.Jump());
        }
    }
}