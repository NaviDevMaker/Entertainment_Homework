using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Game.Player
{
    public class PlayerBehaviourBase : MonoBehaviour
    {
        protected PlayerController _player;
        protected  Animator _animator;
        private void Awake()
        {
            Initialize();
        }
        protected virtual void Initialize()
        {
            SetAnimator();
        }
        void SetAnimator()
        {
            //var rootObj = gameObject;

            //while (rootObj.transform.parent != null)
            //{
            //    rootObj = rootObj.transform.parent.gameObject;
            //}
           
            if(!this.TryGetComponentInParent<PlayerController>(out var player))
                throw new System.Exception("Player  is NULL!!");
            else _player = player;
            if(!this.TryGetComponentInParent<Animator>(out var animator))
                throw new System.Exception("Animator is NULL!!");
            else _animator = animator;
            //_player = //rootObj.GetComponent<PlayerController>();
            //_animator = //rootObj.GetComponent<Animator>();
            //if (_player == null || _animator == null) throw new System.Exception("Player or Animator is NULL!!");
        }
    }
}

