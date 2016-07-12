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

    public override void DoBeforeEnter() 
    {
        // Show logo and menu view
        gameDirector.EnterMainMenuState();
    }

    public override void DoBeforeExit()
    {
        // Hide logo and menu view        
        gameDirector.ExitMainMenuState();
    }
}