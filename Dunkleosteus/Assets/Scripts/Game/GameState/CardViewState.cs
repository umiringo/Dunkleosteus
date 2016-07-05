using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewState : FiniteState
{
    public GameDirector gameDirector;

    public CardViewState(GameDirector director)
    {
        gameDirector = director;
        _stateID = StateID.CardView;
    }

    public virtual void DoBeforeEnter() 
    {
    
    }

    public virtual void DoBeforeExit()
    {
      
    }
}