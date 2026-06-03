using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        PlayerController _player;
        Animator _animator;
        Vector2 _moveInput;
        Vector3 _currentDir;

        readonly int _walkHash = Animator.StringToHash("isWalking");
        [SerializeField] float _rotateSpeed = 20;

        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            SetAnimator();
        }

        void SetAnimator()
        {
            var rootObj = gameObject;

            while (rootObj.transform.parent != null)
            {
                rootObj = rootObj.transform.parent.gameObject;
            }

            _player = rootObj.GetComponent<PlayerController>();
            _animator = rootObj.GetComponent<Animator>();
            if (_player == null || _animator == null) throw new System.Exception("Player or Animator is NULL!!");
        }

        public void Move()
        {
            _animator.SetBool(_walkHash, true);
        }

        public void CancelMove()
        {
            _animator.SetBool(_walkHash, false);
        }

        public void OnMove(InputAction.CallbackContext context)
            => _moveInput = context.ReadValue<Vector2>();

        public void Rotate(out Quaternion targetRot)
        {
            targetRot = Quaternion.LookRotation(_currentDir, Vector3.up);
            _player.transform.rotation = Quaternion.RotateTowards(
                _player.transform.rotation,
                targetRot,
                _rotateSpeed * Time.deltaTime
                );
        }

        public Vector2 GetMoveInput()
            => _moveInput;

        public float GetRotateSpeed()
            => _rotateSpeed;
        public bool IsRotatable(Vector2 input)
        {
            _currentDir = new Vector3(input.x, 0, input.y).normalized;
            return _currentDir.sqrMagnitude > 0.001f;
        }
    }
}
