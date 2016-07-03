using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FiniteStateEvent
{
    protected FiniteStateMachine.EnterState enterDelegate;
    protected FiniteStateMachine.PushState pushDelegate;
    protected FiniteStateMachine.PopState popDelegate;

    protected enum EventType
    {
        None,
        Enter,
        Push,
        Pop
    };

    protected string eventName;
    protected FiniteState stateOwner;
    protected string targetState;
    protected FiniteStateMachine owner;
    protected EventType type;
    public Func<object, object, object, bool> action = null;

    public FiniteStateEvent(string name, FiniteState state, string target, FiniteStateMachine fsm, FiniteStateMachine.EnterState enter, FiniteStateMachine.PushState push, FiniteStateMachine.PopState pop)
    {
        eventName = name;
        stateOwner = state;
        targetState = target;
        owner = fsm;
        type = EventType.None;
        enterDelegate = enter;
        pushDelegate = push;
        popDelegate = pop;
    }

    public FiniteState Enter(string stateName)
    {
        targetState = stateName;
        type = EventType.Enter;
        return stateOwner;
    }

    public FiniteState Push(string stateName)
    {
        targetState = stateName;
        type = EventType.Push;
        return stateOwner;
    }

    public void Pop()
    {
        type = EventType.Pop;
    }

    public void Execute(object o1, object o2, object o3)
    {

    }
}
