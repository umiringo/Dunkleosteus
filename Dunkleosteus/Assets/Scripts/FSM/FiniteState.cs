using UnityEngine;
using System;
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
    protected Dictionary<string, FiniteStateEvent> transferEventMap;

    public FiniteState(IState obj,string name, FiniteStateMachine owner, FiniteStateMachine.EnterState enter, FiniteStateMachine.PushState push, FiniteStateMachine.PopState pop)
    {
        _stateObject = obj;
        _stateName = name;
        _owner = owner;
        enterDelegate = enter;
        pushDelegate = push;
        popDelegate = pop;
        transferEventMap = new Dictionary<string, FiniteStateEvent>();
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
        transferEventMap.Add(eventName, newEvent);
        return newEvent;
    }

    public void Trigger(string eventName)
    {
        transferEventMap[eventName].Execute(null, null, null);
    }

    public void Trigger(string eventName, object param)
    {
        transferEventMap[eventName].Execute(param, null, null);
    }

    public void Trigger(string eventName, object param1, object param2)
    {
        transferEventMap[eventName].Execute(param1, param2, null);
    }

    public void Trigger(string eventName, object param1, object param2, object param3)
    {
        transferEventMap[eventName].Execute(param1, param2, param3);
    }

    public FiniteStateEvent On<T>(string eventName, Func<T, bool> action)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, obejct o3) {
            T param;
            try {
                param = (T)o1;
            }
            catch {
                param = default(T);
            }
            action(param);
            return true;
        };
       transferEventMap.Add(eventName, newEvent);
       return this;
    }

    public FiniteStateEvent On<T>(string eventName, Action<T> action)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, object o3) {
            T param;
            try {
                param = (T)o1;
            }
            catch {
                param = default(T);
            }
            action(param);
            return true;
        };
        transferEventMap.Add(eventName, newEvent);
        return this;
    }

    public FiniteStateEvent On<T1, T2>(string eventName, Func<T1, T2, bool> action) 
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            action(param1, param2);
            return true;
        };
        transferEventMap.Add(eventName, newEvent);
        return this;
    }

    public FiniteStateEvent On<T1, T2>(string eventName, Action<T1, T2> action) 
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            action(param1, param2);
            return true;
        };
        transferEventMap.Add(eventName, newEvent);
        return this;
    }

    public FiniteStateEvent On<T1, T2, T3>(string eventName, Func<T1, T2, T3, bool> action)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            T3 param3;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            try {
                param3 = (T3)o3;
            }
            catch {
                param3 = default(T3);
            }
            action(param1, param2, param3);
            return true;
        };
        transferEventMap.Add(eventName, newEvent);
        return this;
    }

    public FiniteStateEvent On<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        FiniteStateEvent newEvent = new FiniteStateEvent(eventName, null, this, _owner, enterDelegate, pushDelegate, popDelegate);
        newEvent.action = delegate(object o1, object o2, object o3) {
            T1 param1;
            T2 param2;
            T3 param3;
            try {
                param1 = (T1)o1;
            }
            catch {
                param1 = default(T1);
            }
            try {
                param2 = (T2)o2;
            }
            catch {
                param2 = default(T2);
            }
            try {
                param3 = (T3)o3;
            }
            catch {
                param3 = default(T3);
            }
            action(param1, param2, param3);
            return true;
        };
        transferEventMap.Add(eventName, newEvent);
        return this;       
    }

}
