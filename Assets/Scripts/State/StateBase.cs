using UnityEngine;
using Game.Creature;

public abstract class StateBase<TOwner> : IState<TOwner>
{
     public StateBase(TOwner owner) => Owner = owner;
     public TOwner Owner { get; private set;}
     public abstract void OnUpdate();
     public abstract void OnEnter();
     public abstract void OnExit();
}


