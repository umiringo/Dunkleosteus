using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IState
{
    void OnEnter(string preState);
    void OnExit(string nextState);
    void OnUpdate();
}

public class FiniteState
{
    protected FiniteStateMachine.EnterState enterDelegate;
    protected FiniteStateMachine.PushState pushDelegate;
    protected FiniteStateMachine.PopState popDelegate;

    protected IState _stateObject;
    protected string _stateName;
    protected FiniteStateMachine _owner;
    protected Dictionary<string, FiniteStateEvent> transferEventDic;

    public FiniteState(IState obj,string name, FiniteStateMachine owner, FiniteStateMachine.EnterState enter, FiniteStateMachine.PushState push, FiniteStateMachine.PopState pop)
    {
        _stateObject = obj;
        _stateName = name;
        _owner = owner;
        enterDelegate = enter;
        pushDelegate = push;
        popDelegate = pop;
        transferEventDic = new Dictionary<string, FiniteStateEvent>();
    }

    public IState StateObject
    {
        get
        {
            return _stateObject;
        }
    }

    public string StateName
    {
        get
        {
            return _stateName;
        }
    }
    
    public FiniteStateEvent On(string eventName)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        transferEventDic.Add(eventName, newEvent);
        return newEvent;
    }

    public void Trigger(string eventName)
    {
        transferEventDic[eventName].Execute(null, null, null);
    }

    public void Trigger(string eventName, object param)
    {
        transferEventDic[eventName].Execute(param, null, null);
    }

    public void Trigger(string eventName, object param1, object param2)
    {
        transferEventDic[eventName].Execute(param1, param2, null);
    }

    public void Trigger(string eventName, object param1, object param2, object param3)
    {
        transferEventDic[eventName].Execute(param1, param2, param3);
    }

    public FiniteStateEvent On<T>(string eventName, Func<T, bool> action)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
       
    }
}
