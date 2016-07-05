using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : FiniteState
{
    public GameDirector gameDirector;

    public MainMenuState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.MainMenu;
    }

    public virtual void DoBeforeEnter() 
    {
        // Show logo and menu View 
    }

    public virtual void DoBeforeExit()
    {
        // Hide logo and menu view        
    }
}