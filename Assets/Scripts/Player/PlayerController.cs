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

        public StateMachine StateMachine { get; private set; }
        public Func<Quaternion, float,UniTask> OnRotatePlayerAsync;
        // Start is called once before the first execution of OnUpdate after the MonoBehaviour is created
        void Start()
        {
            Initialize();
        }

        // OnUpdate is called once per frame
        void Update()
        {
            StateMachine.OnUpdate();
        }

        void Initialize()
        {
            StateMachine = new StateMachine
                (
                    new IdleState(this),
                    new MoveState(this)
                );
            StateMachine.ChangeState(StateType.Idle);
        }
    }

    [Serializable]
    public class PlayerComponent
    {
        [field: SerializeField] public PlayerMovement PlayerMovement;
        [field: SerializeField] public PlayerIdleBehaviour PlayerIdleBehaviour;
    }
}


