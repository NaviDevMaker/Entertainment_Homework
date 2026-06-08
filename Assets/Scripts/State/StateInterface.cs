using UnityEngine;
using UnityEngine.Experimental.AI;


namespace Game.Creature
{

    public interface IIdleState { };
    public interface IMoveState<TOwner> { void Move(); };

    public interface IState<TOwner>
    {
        TOwner Owner { get; }
        void OnUpdate();
        void OnEnter();
        void OnExit();
    }
    public enum StateType
    {
        Idle,
        Move
    }
}


