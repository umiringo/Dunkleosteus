using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayMgr gamePlay;
    public GameContainer gameContainer;
    public GameDirector gameDirector;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    #region Trigger by PanelLevel
    public void OnStarTouched(GameObject go)
    {
        gamePlay.TriggerStar(go);
    }

    public void OnCardTouched(GameObject go)
    {
        gameContainer.BeginToFlop(go);
    }

    public void OnGameWin()
    {
        // Do win logic
        gamePlay.OnGameWin();
    }

    public void OnWinTweenPlayed()
    {
        gamePlay.ShowComplete();
    }

    public void OnDetailTriggered()
    {
        gamePlay.ShowMenu();
    }

    public void OnTouchNext()
    {
        gamePlay.OnNextLevel();
    }

    public void OnTouchBack()
    {
        gamePlay.OnBackToLevelSelect();
    }

    public void OnTouchTips()
    {
        gamePlay.OnTips();
    }
    #endregion
}
