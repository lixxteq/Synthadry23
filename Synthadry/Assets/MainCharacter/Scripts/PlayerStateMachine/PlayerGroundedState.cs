using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory) {
        _isRootState = true;
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
        if (!_context.IsMoving && !_context.IsRunning) {
            SetSubState(_stateFactory.Idle());
        }
        else if (_context.IsMoving && !_context.IsRunning) {
            SetSubState(_stateFactory.Walk());
        }
        else {
            SetSubState(_stateFactory.Run());
        }
    }
    public override void CheckSwitchStates()
    {
        if (_context.IsJumping) {
            SwitchState(_stateFactory.Jump());
        }
    }
}