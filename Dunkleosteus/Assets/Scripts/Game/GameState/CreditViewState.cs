using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditViewState : FiniteState
{
    public GameDirector gameDirector;

    public CreditViewState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.CreditView;
    }

    public virtual void DoBeforeEnter() 
    {
    
    }

    public virtual void DoBeforeExit()
    {
      
    }
}