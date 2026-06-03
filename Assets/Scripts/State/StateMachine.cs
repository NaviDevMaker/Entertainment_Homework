using Game.Player;
using UnityEngine;

namespace Game.Creature
{
    public sealed class StateMachine
    {
        public StateMachine(IState<PlayerController> idleState,IState<PlayerController> moveState)
        {
            _idleState = idleState;
            _moveState = moveState;
        }

        IState<PlayerController> _currentState = null;
        readonly IState<PlayerController> _idleState;
        readonly IState<PlayerController> _moveState;
        public void ChangeState(StateType nextStateType)
        {
            _currentState?.OnExit();
            var nextState = GetState(nextStateType);
            _currentState = nextState;
            _currentState.OnEnter();
        }

        public void OnUpdate(PlayerController owner)
        {
            _currentState?.OnUpdate(owner);
        }
        IState<PlayerController> GetState(StateType nextState)
        {
            return nextState switch
            {
                StateType.Idle => _idleState,
                StateType.Move => _moveState,
                _ => throw new System.Exception("Doesn't exist state!!")
            };
        }
    }
}


