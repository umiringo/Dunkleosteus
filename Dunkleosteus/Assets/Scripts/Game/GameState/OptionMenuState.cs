using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuState : FiniteState
{
    public GameDirector gameDirector;

    public OptionMenuState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.OptionMenu;
    }

    public override void DoBeforeEnter() 
    {
    
    }

    public override void DoBeforeExit()
    {
      
    }
}