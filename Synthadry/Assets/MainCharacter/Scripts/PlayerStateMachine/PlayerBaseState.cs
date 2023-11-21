using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlayerBaseState
{
    protected bool _isRootState = false;
    protected CustomCharacterController _context;
    protected PlayerStateFactory _stateFactory;
    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;

    public PlayerBaseState(CustomCharacterController context, PlayerStateFactory stateFactory)
    {
        _context = context;
        _stateFactory = stateFactory;
    }

    // Вызывается перед активацией состояния
    public abstract void EnterState();
    // То же что и MonoBehaviour.Update() (вызывается каждый кадр), только для текущего состояния
    public abstract void UpdateState();
    // То же что и MonoBehaviour.FixedUpdate()
    public abstract void FixedUpdateState();
    // Вызывается перед переходом в другое состояние
    public abstract void ExitState();
    // Вызывается в UpdateState() у активного состояния, каждый кадр проверяет условия для смены состояния на другое
    public abstract void CheckSwitchStates();
    // Метод смены состояния, выходит из текущего состояния и входит в следующее
    protected void SwitchState(PlayerBaseState nextState) {
        ExitState();
        nextState.EnterState();
        if (_isRootState) {
            _context.CurrentState = nextState;
        }
        else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(nextState);
        }
    }

    public abstract void InitSubState();

    protected void SetSubState(PlayerBaseState subState) {
        _currentSubState = subState;
        subState.SetSuperState(this);
    }

    protected void SetSuperState(PlayerBaseState superState) {
        _currentSuperState = superState;
    }

    public void UpdateStates() {
        UpdateState();
        if (_currentSubState != null) {
            _currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates() {
        FixedUpdateState();
        if (_currentSubState != null) {
            _currentSubState.FixedUpdateStates();
        }
    }
}

