using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Creature;

namespace Game.Player
{
    public sealed class MoveState : StateBase<PlayerController>,IMoveState<PlayerController>
    {
        public MoveState(PlayerController owner):base(owner) { }
        public void Move()
        {
            Owner.PlayerComponent.PlayerMovement.Move();
        }
        public override void OnEnter()
        {
            Owner.PlayerComponent.PlayerMovement.UpdateAnimator();
        }
        public override void OnExit()
        {
            
        } 
        public override void OnUpdate()
        {
            var movement = Owner.PlayerComponent.PlayerMovement;
            var moveInput = movement.GetMoveInput();

            if (movement.IsRotatable(moveInput))
            {
                var targetRot = default(Quaternion);
                movement.Rotate(out targetRot);
                Move();
                Owner.OnRotatePlayerAsync(targetRot, movement.GetRotateSpeed()).Forget();
            }
            else Owner.StateMachine.ChangeState(StateType.Idle);
        }
    }
}


