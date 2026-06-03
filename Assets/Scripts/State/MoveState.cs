using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Creature;

namespace Game.Player
{
    public sealed class MoveState : StateBase<PlayerController>,IMoveState<PlayerController>
    {
        public void Move(PlayerController owner)
        {
            owner.PlayerComponent.PlayerMovement.Move();
        }

        public override void OnEnter()
        {
           
        }

        public override void OnExit()
        {
            
        } 

        public override void OnUpdate(PlayerController owner)
        {
            var movement = owner.PlayerComponent.PlayerMovement;
            var moveInput = movement.GetMoveInput();

            if(movement.IsRotatable(moveInput))
            {
                var targetRot = default(Quaternion);
                movement.Rotate(out targetRot);
                owner.OnRotatePlayerAsync(targetRot, movement.GetRotateSpeed()).Forget();
                Move(owner);
            }
        }

    }
}


