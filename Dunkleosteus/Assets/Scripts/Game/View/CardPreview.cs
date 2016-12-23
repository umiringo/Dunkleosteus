using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class CardPreview : MonoBehaviour {
    public UILocalize labelTitle;
    private GameObject _card;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(string levelName) {
        labelTitle.key = "LK" + levelName + "Title";
        Debug.Log("CardPreview.Init name = " + PathContainer.CardPath + levelName + "Card" );
        _card = Instantiate(Resources.Load(PathContainer.CardPath + levelName + "Card")) as GameObject;
        _card.transform.parent = this.gameObject.transform;
        _card.transform.localPosition = Vector3.zero;
        _card.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
        _card.GetComponent<Card>().Init(levelName);
    }

    public void ClearCard()
    {
        Destroy(_card);
    }

    public void FlopCard(GameObject go)
    {
        _card.GetComponent<Card>().BeginToFlop(go);
    }
}
