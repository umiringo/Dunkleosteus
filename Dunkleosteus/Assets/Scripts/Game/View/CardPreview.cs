using UnityEngine;
using System.Collections;

public class CardPreview : MonoBehaviour {
    public UILocalize labelTitle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(string levelName) {
        labelTitle.key = "LK" + levelName + "Title";
    }
}
