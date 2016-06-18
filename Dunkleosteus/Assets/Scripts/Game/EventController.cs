using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

    public LevelPlayMgr gamePlay;
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnStarClicked(GameObject go)
    {
        gamePlay.OnStarClicked(go);
    }
}
