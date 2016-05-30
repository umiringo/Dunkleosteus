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
        if(go.GetComponent<Star>().state == StarState.NORMAL)
        {
            go.GetComponent<Star>().UpdateStatus(StarState.CHOSEN);
        }
        else
        {
            go.GetComponent<Star>().UpdateStatus(StarState.NORMAL);
        }
        
    }
}
