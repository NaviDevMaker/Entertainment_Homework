using UnityEngine;
using Game.Creature;
public abstract class StateBase<TOwner>:IState<TOwner>
{
    public abstract void OnUpdate(TOwner owner);
    public abstract void OnEnter();
    public abstract void OnExit();  
}
