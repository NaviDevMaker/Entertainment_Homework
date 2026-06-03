using UnityEngine;
using Game.Creature;
using System;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
namespace Game.Player
{
    public sealed class PlayerController : MonoBehaviour
    { 
        [field:SerializeField] public PlayerComponent PlayerComponent;    

        StateMachine stateMachine;
        public Func<Quaternion, float,UniTask> OnRotatePlayerAsync;
        // Start is called once before the first execution of OnUpdate after the MonoBehaviour is created
        void Start()
        {
            Initialize();
        }

        // OnUpdate is called once per frame
        void Update()
        {
            stateMachine.OnUpdate(this);
        }

        void Initialize()
        {
            stateMachine = new StateMachine
                (
                    new IdleState(),
                    new MoveState()
                );
            stateMachine.ChangeState(StateType.Idle);
        }
    }

    [Serializable]
    public class PlayerComponent
    {
        [field: SerializeField] public PlayerMovement PlayerMovement;
    }
}


