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
        _context.Animator.SetBool("IsGrounded", true);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        // workaround for falling w/o jump
        if (!_context.CharacterController.isGrounded)
        {
            _context._currentVelocity.y += _context.gravity * Time.deltaTime;
            Vector3 gravityMove = new Vector3(0, _context._currentVelocity.y, 0);
            _context.CharacterController.Move(gravityMove * Time.deltaTime);
        }
    }
    public override void FixedUpdateState()
    {
        
    }
    public override void ExitState()
    {
        Debug.Log("PState: exit Grounded");
        _context.Animator.SetBool("IsGrounded", false);
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