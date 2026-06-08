using UnityEngine;
using Game.Creature;

namespace Game.Player
{
    public sealed class IdleState : StateBase<PlayerController>,IIdleState
    {
        public IdleState(PlayerController owner) : base(owner) { }
        public override void OnEnter()
        {
            IsIdle();
        }
        public override void OnExit()
        {
            
        }
        public override void OnUpdate()
        {
            var movement = Owner.PlayerComponent.PlayerMovement;
            if (movement.IsMovable()) Owner.StateMachine.ChangeState(StateType.Move);
        }
        void IsIdle()
        {
            Owner.PlayerComponent.PlayerIdleBehaviour.IsIdle();
        }
    }
}


