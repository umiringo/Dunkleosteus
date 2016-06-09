using UnityEngine;
using System.Collections;
using GlobalDefines;

public class EventController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //TODO jft
    public void OnStarClicked(GameObject go, int index)
    {
        Debug.Log("Index = " + index + " star be clicked!");
        if(go.GetComponent<Star>().state == StarState.Normal)
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Chosen);
        }
        else if(go.GetComponent<Star>().state == StarState.Chosen)
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Connected);
        }
        else
        {
            go.GetComponent<Star>().UpdateStatus(StarState.Normal);
        }
        
    }
}
