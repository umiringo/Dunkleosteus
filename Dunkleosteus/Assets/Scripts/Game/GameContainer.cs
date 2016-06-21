using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {

	// Use this for initialization
    public GameObject starChart;
	void Start () 
    {
        starChart.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void GameWin()
    {
        starChart.SetActive(true);
    }
}
