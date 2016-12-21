using UnityEngine;
using System.Collections;

public class CardPreview : MonoBehaviour {
    public UILocalize labelTitle;
    public GameObject _card;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(string levelName) {
        labelTitle.key = "LK" + levelName + "Title";
    }

    public void FlopCard(GameObject go)
    {
        _card.GetComponent<Card>().BeginToFlop(go);
    }
}
