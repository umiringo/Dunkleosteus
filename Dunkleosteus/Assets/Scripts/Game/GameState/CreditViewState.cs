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

    public override void DoBeforeEnter() 
    {
    
    }

    public override void DoBeforeExit()
    {
      
    }
}