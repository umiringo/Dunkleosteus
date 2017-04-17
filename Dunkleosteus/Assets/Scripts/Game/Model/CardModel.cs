using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour {
    public GameObject cardPreviewGameObject;
    public CardPreview cardPreview;
    public GameObject cardListGameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowCardPreview(string level)
    {
        cardPreview.Init(level);
        cardPreview.GetComponent<MoveInOut>().MoveIn();
    }

    public void CloseCardPreview()
    {
        cardPreview.GetComponent<MoveInOut>().MoveOut();
        cardPreview.ClearPreview();
    }

    public void TriggerCardPreview(GameObject go)
    {
        cardPreview.FlopCard(go);
    }
}
