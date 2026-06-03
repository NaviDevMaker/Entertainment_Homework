using UnityEngine;
using UnityEngine.Experimental.AI;


namespace Game.Creature
{

    public interface IIdleState { };
    public interface IMoveState<TOwner> { void Move(TOwner owner); };

    public interface IState<TOwner>
    {
        void OnUpdate(TOwner owner);
        void OnEnter();
        void OnExit();
    }
    public enum StateType
    {
        Idle,
        Move
    }
}


