using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTransition
{
    NullTransition = 0,
    PressStart = 1,
    ViewOption = 2,
    ViewCredit = 3,
    ViewCard = 4,
    ChoseLevel = 5,
    BackToLevelSelect = 6,
}

public enum StateID
{
    NullStateID = 0,
    MainMenu = 1,
    LevelSelect = 2,
    OptionMenu = 3,
    CreditView = 4,
    CardView = 5,
    GameScene = 6,
}

public abstract class FiniteState
{
    protected Dictionary<StateTransition, StateID> _stateTransitionMap = new Dictionary<StateTransition, StateID>();
    protected StateID _stateID;
    public StateID ID { get { return _stateID; }}

    public void AddTransition(StateTransition trans, StateID id)
    {
        if( trans == StateTransition.NullTransition ) {
            Debug.LogError("FiniteState Error: NullTransition is not allowed for a real transition");
            return;
        }
        if( id == StateID.NullStateID ) {
            Debug.LogError("FiniteState Error: NullStateID is not allowed for a real ID");
            return;
        }

        if( _stateTransitionMap.ContainsKey(trans) ) {
            Debug.LogError("FiniteState Error: State " + _stateID.ToString() + " already has transition " + trans.ToString() + "! Cant Insert another one !");
            return;
        }
        _stateTransitionMap.Add(trans, id);
    }

    public void DeleteTransition(StateTransition trans)
    {
        if( trans == StateTransition.NullTransition )
        {
            Debug.LogError("FiniteState Error: NullTransition is not allowed for a real transition");
            return;
        }

        if(_stateTransitionMap.ContainsKey(trans))
        {
            _stateTransitionMap.Remove(trans);
            return;
        }
        Debug.LogError("FiniteState Error: Transition " + trans.ToString() + " - " + _stateID.ToString() + " was not existed! ");
    }

    public StateID GetTargetState(StateTransition trans)
    {
        if(_stateTransitionMap.ContainsKey(trans))
        {
            return _stateTransitionMap[trans];
        }
        return StateID.NullStateID;
    }

    public virtual void DoBeforeEnter() {}
    public virtual void DoBeforeExit() {}

    // public abstract void Reason(GameObject go);
    // public abstract void Act(GameObject go);
}