using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    private TweenRotation tweenRotation;
    private int step;
	// Use this for initialization
	void Start () {
        iTween.ShakeRotation(gameObject, new Vector3(0, 80, 0), 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SecondStep()
    {

    }
}
