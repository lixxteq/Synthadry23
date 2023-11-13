using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory){}
    public override void EnterState()
    {
        
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
        _context._anim.SetBool("RifleRunning", false);
        _context._anim.SetBool("isRunning", false);
    }
    public override void InitSubState() {

    }
    public override void CheckSwitchStates()
    {
        
    }
}