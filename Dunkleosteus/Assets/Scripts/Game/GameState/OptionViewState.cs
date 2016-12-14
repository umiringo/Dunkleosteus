using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionViewState : FiniteState
{
    public GameDirector gameDirector;

    public OptionViewState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.OptionView;
    }

    public override void DoBeforeEnter() 
    {
        gameDirector.EnterOptionViewState();
    }

    public override void DoBeforeExit()
    {
        gameDirector.ExitOptionViewState();
    }
}