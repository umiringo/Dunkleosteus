using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private List<FiniteState> _stateList;

    private StateID _currentStateID;
    public StateID CurrentStateID { get { return _currentStateID; } }

    private FiniteState _currentState;
    public FiniteState CurretnState { get { return _currentState; } }

    public FiniteStateMachine()
    {
        _stateList = new List<FiniteState>();
        _currentStateID = StateID.NullStateID;
    }

    public void AddFiniteState(FiniteState fs)
    {
        if( fs == null ) {
            Debug.LogError("FiniteStateMachine Error: Null reference is not allowed");
            return;
        }

        if( _stateList.Count == 0 ) {
            _stateList.Add(fs);
            _currentState = fs;
            _currentStateID = fs.ID;
            return;
        }

        foreach( FiniteState fState in _stateList ) {
            if( fState.ID == fs.ID ) {
                Debug.LogError("FiniteStateMachine Error: Impossible to add state " + fs.ID.ToString() + " because state has already been added !");
                return;
            }
        }
        _stateList.Add(fs);
    }

    public void DeleteFiniteState(StateID id)
    {
        if( id == StateID.NullStateID ) {
            Debug.LogError("FiniteStateMachine Error: NullStateID is not allowed for a real state!");
            return;
        }

        foreach( FiniteState fs in _stateList ) {
            if( fs.ID == id ) {
                _stateList.Remove(fs);
                return;
            }
        }
        Debug.LogError("FiniteStateMachine Error: Impossible to delete state " + id.ToString() + " because state was not existed!");
    }

    public void PerformTransition(StateTransition trans)
    {
        if( trans == StateTransition.NullTransition ) {
            Debug.LogError("FiniteStateMachine Error: NullTransition is not allowed for a real transition!");
            return;
        }

        StateID id = _currentState.GetTargetState(trans);
        if( id == StateID.NullStateID ) {
            Debug.LogError("FiniteStateMachine Error: " + _currentStateID.ToString() + " does not have a target state for transition " + trans.ToString() + "!");
            return;
        }

        _currentStateID = id;
        foreach (FiniteState fs in _stateList) {
            if( fs.ID == _currentStateID ) {
                _currentState.DoBeforeExit();
                _currentState = fs;
                _currentState.DoBeforeEnter();
                break;
            }
        }
    }
}