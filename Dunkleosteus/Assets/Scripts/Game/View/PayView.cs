using UnityEngine;
using System.Collections;

public class PayView : MonoBehaviour {
    public UILabel labelCoin;
    public GameDirector gameDirector;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RefreshView()
    {
        labelCoin.text = gameDirector.GetCoin().ToString();
    }
}
