using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public sealed class PlayerIdleBehaviour : PlayerBehaviourBase
    {
        bool _isChangable = false;
        public void IsIdle()
        {
            _animator.SetBool(PlayerAnimatorHashes.walkHash, false);
        }

        public void OnMove(InputAction.CallbackContext context)
            => _isChangable = context.ReadValue<Vector2>().sqrMagnitude > 0.0001f;
        public bool IsChangable()
            => _isChangable;
    }

}

