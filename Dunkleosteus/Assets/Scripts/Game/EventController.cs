using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayMgr gamePlay;
    public GameContainer gameContainer;
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

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
        gameContainer.GameWin();
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

}
