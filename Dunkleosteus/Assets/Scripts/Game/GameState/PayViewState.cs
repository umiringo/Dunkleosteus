using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayViewState : FiniteState
{
    public GameDirector gameDirector;

    public PayViewState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.PayView;
    }

    public override void DoBeforeEnter() 
    {
        gameDirector.EnterPayViewState();
    }

    public override void DoBeforeExit()
    {
        gameDirector.ExitPayViewState();
    }
}