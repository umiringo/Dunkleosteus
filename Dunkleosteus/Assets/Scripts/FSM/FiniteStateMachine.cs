using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FiniteStateMachine
{
    public delegate void EnterState(string stateName);
    public delegate void PushState(string stateName, string lastStateName);
    public delegate void PopState();

    protected Dictionary<string, FiniteState> stateDic;
    protected string entryPoint;
    protected Stack<FiniteState> stateStack;

    public FiniteState State(string stateName)
    {
        return stateDic[stateName];
    }

    public void EntryPoint(string startName)
    {
        entryPoint = startName;
    }

    public FiniteState CurrentState
    {
        get
        {
            if (stateStack.Count <= 0) {
                return null;
            }
            return stateStack.Peek();
        }
    }

    public FiniteStateMachine()
    {
        stateDic = new Dictionary<string, FiniteState>();
        entryPoint = null;
        stateStack = new Stack<FiniteState>();
    }

    public void Register(string stateName, IState stateObject)
    {
        if (stateDic.Count == 0) {
            entryPoint = stateName;
        }
        stateDic.Add(stateName, new FiniteState(stateObject, this, stateName, Enter, PushState, Pop));
    }
    public void Update()
    {
        if (CurrentState == null) {
            stateStack.Push(stateDic[entryPoint]);
            CurrentState.StateObject.OnEnter(null);
        }
        CurrentState.StateObject.OnUpdate();
    }

    protected void Push(string stateName, string lastStateName)
    {
        stateStack.Push(stateDic[stateName]);
        stateStack.Peek().StateObject.OnEnter(lastStateName);
    }

    public void Push(string newState)
    {
        string lastStateName = null;
        if (stateStack.Count > 1) {
            lastStateName = stateStack.Peek().StateName;
        }
        Push(newState, lastStateName);
    }

    public void Enter(string stateName)
    {
        Push(stateName, Pop(stateName));
    }

    public void Pop()
    {
        Pop(null);
    }

    protected string Pop(string newName)
    {
        FiniteState lastState = stateStack.Peek();
        string newStateName = null;
        if (newName == null && stateStack.Count > 1) {
            int index = 0;
            foreach (FiniteState item in stateStack) {
                if (index++ == stateStack.Count - 2) {
                    newStateName = item.StateName;
                }
            }
        }
        else {
            newStateName = newName;
        }
        string lastStateName = null;
        if (lastState != null) {
            lastStateName = lastState.StateName;
            lastState.StateObject.OnExit(newStateName);
        }
        stateStack.Pop();
        return lastStateName;
    }

    public void Trigger(string eventName)
    {
        CurrentState.Trigger(eventName);
    }

    public void Trigger(string eventName, object param)
    {
        CurrentState.Trigger(eventName, param);
    }

    public void Trigger(string eventName, object param1, object param2)
    {
        CurrentState.Trigger(eventName, param1, param2);
    }

    public void Trigger(string eventName, object param1, object param2, object param3)
    {
        CurrentState.Trigger(eventName, param1, param2, param3);
    }
}
