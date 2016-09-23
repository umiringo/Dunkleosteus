using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayMgr gamePlay;
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
        gamePlay.TriggerCard(go);
    }

    public void OnTouchTips()
    {
        gamePlay.OnTips();
    }
    #endregion
}
