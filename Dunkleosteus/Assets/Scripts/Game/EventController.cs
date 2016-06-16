using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        //Debug.Log(GamePlayMgr.Instance.identify);
        GamePlayMgr.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    //TODO jft
    public void OnStarClicked(GameObject go)
    {
        GamePlayMgr.Instance.OnStarClicked(go);
    }
}
