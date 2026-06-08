using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public sealed class PlayerMovement : PlayerBehaviourBase
    {      
        Vector2 _moveInput;
        Vector3 _currentDir;
        Rigidbody _rigidbody;
        [Header("The Player Rotate Speed")]
        [SerializeField,Range(0,200)] float _rotateSpeed = 20f;

        [Header("The Player Speed")]
        [SerializeField, Range(1, 100)] float _moveSpeed = 20f;

        protected override void Initialize()
        {
            base.Initialize();
            if (_player == null) return;
            if (!_player.TryGetComponentInParent<Rigidbody>(out var rigidbody))
            {
                throw new System.Exception("The RIGIDBPDY is NULL!!");
            }
            _rigidbody = rigidbody;
        }
        public void Move()
        {
            if (_rigidbody == null) return;
            var nextPos = _rigidbody.position + (_rigidbody.rotation * Vector3.forward) * _moveSpeed * Time.deltaTime;
            _rigidbody.MovePosition(nextPos);
        }

        public void UpdateAnimator()
            => _animator.SetBool(PlayerAnimatorHashes.walkHash, true);

 
        public void OnMove(InputAction.CallbackContext context)

        {
            Debug.Log("OnMove");
            _moveInput = context.ReadValue<Vector2>();
        }

        public void Rotate(out Quaternion targetRot)
        {
            if (_rigidbody == null)
            {
                targetRot = _player.transform.rotation;
                return;
            }

            targetRot = Quaternion.LookRotation(_currentDir, Vector3.up);
            var nextRot = Quaternion.RotateTowards(
                _rigidbody.rotation,
                targetRot,
                _rotateSpeed * Time.deltaTime
                );
            _rigidbody.MoveRotation(nextRot);
        }

        public Vector2 GetMoveInput()
            => _moveInput;

        public bool IsMovable()
            => _moveInput.sqrMagnitude > 0.0001f;

        public float GetRotateSpeed()
            => _rotateSpeed;
        public bool IsRotatable(Vector2 input)
        {
            _currentDir = new Vector3(input.x, 0, input.y).normalized;
            return _currentDir.sqrMagnitude > 0.001f;
        }
    }
}
