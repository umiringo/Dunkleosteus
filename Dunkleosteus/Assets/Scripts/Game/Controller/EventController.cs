using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayModel levelPlayModel;
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
        levelPlayModel.TriggerStar(go);
    }

    public void OnCardTouched(GameObject go)
    {
        levelPlayModel.TriggerCard(go);
    }

    public void OnTouchTips()
    {
        levelPlayModel.OnTips();
    }
    #endregion

    public void OnStartGame()
    {
        gameDirector.StartGame();
    }

    public void OnSelectLevel(string level)
    {
        gameDirector.SelectLevel(level);
    }

    public void OnBackSelectLevel()
    {
        gameDirector.BackSelectLevel();
    }

    public void OnStartNextLevel()
    {
        string nextLevel = gameDirector.GetNextLevel();
        if(nextLevel == "fin") {
            return;
        } else {
            gameDirector.StartNextLevel();  
        }
    }
}
