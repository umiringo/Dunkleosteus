using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Debug.Log(GamePlayMgr.Instance.identify);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //TODO jft
    public void OnStarClicked(GameObject go)
    {
        Star goStar = go.GetComponent<Star>();
        Debug.Log("Index = " + goStar.index + " star be clicked!");
        Debug.Log(GamePlayMgr.Instance.identify);
        if(go.GetComponent<Star>().state == StarState.Normal)
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Chosen);
        }
        else if(go.GetComponent<Star>().state == StarState.Chosen)
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Linked);
        }
        else
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Normal);
        }
        
    }
}
